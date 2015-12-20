using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    internal struct CacheKey
    {
        public readonly EntityCopier Copier;
        public readonly Type Source;
        public readonly Type Result;
        public CacheKey(EntityCopier copier, Type source, Type result)
        {
            Copier = copier;
            Source = source;
            Result = result;
        }
    }

    public delegate void CopyDelegate<TSource, TResult>(TSource source, ref TResult result);

    public abstract class EntityCopier
    {

        #region Fields
        private static MethodInfo _copyMethod;
        private Dictionary<CacheKey, Delegate> _dict =
            new Dictionary<CacheKey, Delegate>();
        #endregion

        #region Internal Methods

        internal void RegistCore<TSource, TResult>(EntityCopier copier, CopyDelegate<TSource, TResult> cd)
        {
            _dict[new CacheKey(copier, typeof(TSource), typeof(TResult))] = cd;
        }

        internal static void CheckSourceNull(ILGenerator gen, Type sourceType)
        {
            if (!sourceType.IsValueType)
            {
                Label notNull = gen.DefineLabel();
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Brtrue_S, notNull);
                gen.Emit(OpCodes.Ret);
                gen.MarkLabel(notNull);
            }
            else if (sourceType.IsGenericType &&
                sourceType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Label notNull = gen.DefineLabel();
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Box, sourceType);
                gen.Emit(OpCodes.Brtrue_S, notNull);
                gen.Emit(OpCodes.Ret);
                gen.MarkLabel(notNull);
            }
        }

        #endregion

        #region Virtual Methods

        public abstract void Copy<TSource, TResult>(TSource source, ref TResult result);

        internal protected virtual bool CopyCore<TSource, TResult>(EntityCopier copier, TSource source, ref TResult result)
        {
            CopyDelegate<TSource, TResult> cd = GetDelegate<TSource, TResult>(copier);
            if (cd == null)
                return false;
            cd(source, ref result);
            return true;
        }

        internal protected abstract CopyDelegate<TSource, TResult> CreateCopyDelegate<TSource, TResult>(EntityCopier copier);

        #endregion

        #region Properties

        protected static MethodInfo CopyMethod
        {
            get
            {
                return _copyMethod ??
                    (_copyMethod = typeof(EntityCopier).GetMethod("Copy"));
            }
        }

        public virtual bool CanBeDecorated
        {
            get { return true; }
        }

        #endregion

        #region Private Implements

        private CopyDelegate<TSource, TResult> GetDelegate<TSource, TResult>(EntityCopier copier)
        {
            Delegate d = null;
            CacheKey key = new CacheKey(copier, typeof(TSource), typeof(TResult));
            if (_dict.TryGetValue(key, out d))
                return (CopyDelegate<TSource, TResult>)d;
            CopyDelegate<TSource, TResult> cd = CreateCopyDelegate<TSource, TResult>(copier);
            _dict[key] = cd;
            return cd;
        }

        #endregion

        #region Static Methods

        public static EntityCopier CreateAuto()
        {
            return new AutoCopier();
        }

        protected static bool CheckType(Type type)
        {
            return type.IsVisible;
        }

        #endregion

    }

    //以及自动复制的实现（估计看了就会头晕，呵呵）
    internal class AutoCopier
        : EntityCopier
    {

        #region Sub-Classes

        protected sealed class ContextInfo
        {
            internal EntityCopier Copier;
            internal Type SourceType;
            internal Type ResultType;
            internal ILGenerator ILGen;
            internal bool SourceIsValueType;
            internal bool ResultIsValueType;
        }

        protected struct PropertyPair
        {
            public readonly PropertyInfo SourceProperty;
            public readonly PropertyInfo ResultProperty;
            public PropertyPair(PropertyInfo sourceProperty, PropertyInfo resultProperty)
            {
                SourceProperty = sourceProperty;
                ResultProperty = resultProperty;
            }
        }

        #endregion

        #region Overrides

        public sealed override void Copy<TSource, TResult>(TSource source, ref TResult result)
        {
            if (!CheckType(typeof(TSource)))
                throw new ArgumentException("type cannot be non-public type", typeof(TSource).ToString());
            if (!CheckType(typeof(TResult)))
                throw new ArgumentException("type cannot be non-public type", typeof(TResult).ToString());
            CopyCore<TSource, TResult>(this, source, ref result);
        }

        protected internal sealed override CopyDelegate<TSource, TResult> CreateCopyDelegate<TSource, TResult>(EntityCopier copier)
        {
            ContextInfo info = new ContextInfo();
            info.Copier = copier;
            info.SourceType = typeof(TSource);
            info.ResultType = typeof(TResult);
            info.SourceIsValueType = info.SourceType.IsValueType;
            info.ResultIsValueType = info.ResultType.IsValueType;
            return (CopyDelegate<TSource, TResult>)CreateCopyDelegateCore(info);
        }

        #endregion

        #region Private Implements

        private Delegate CreateCopyDelegateCore(ContextInfo info)
        {
            Type[] args = new Type[3];
            args[0] = typeof(EntityCopier);
            args[1] = info.SourceType;
            args[2] = info.ResultType.MakeByRefType();
            DynamicMethod dm = new DynamicMethod(string.Empty, typeof(void), args);
            info.ILGen = dm.GetILGenerator();
            CreateCopyDelegateBody(info);
            info.ILGen.Emit(OpCodes.Ret);
            return dm.CreateDelegate(typeof(CopyDelegate<,>).MakeGenericType(info.SourceType, info.ResultType), info.Copier);
        }

        private void CreateCopyDelegateBody(ContextInfo info)
        {
            CheckSourceNull(info);
            if (info.SourceType == info.ResultType)
                CopySameType(info);
            else
                CopyDifferentType(info);
            info.ILGen.Emit(OpCodes.Ret);
        }

        private static void CheckSourceNull(ContextInfo info)
        {
            if (!info.SourceIsValueType)
            {
                Label notNull = info.ILGen.DefineLabel();
                info.ILGen.Emit(OpCodes.Ldarg_1);
                info.ILGen.Emit(OpCodes.Brtrue_S, notNull);
                info.ILGen.Emit(OpCodes.Ret);
                info.ILGen.MarkLabel(notNull);
            }
            else if (info.SourceType.IsGenericType &&
                info.SourceType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Label notNull = info.ILGen.DefineLabel();
                info.ILGen.Emit(OpCodes.Ldarg_1);
                info.ILGen.Emit(OpCodes.Box, info.SourceType);
                info.ILGen.Emit(OpCodes.Brtrue_S, notNull);
                info.ILGen.Emit(OpCodes.Ret);
                info.ILGen.MarkLabel(notNull);
            }
        }

        private static void CopySameType(ContextInfo info)
        {
            info.ILGen.Emit(OpCodes.Ldarg_2);
            info.ILGen.Emit(OpCodes.Ldarg_1);
            info.ILGen.Emit(OpCodes.Stobj, info.ResultType);
        }

        private void CopyDifferentType(ContextInfo info)
        {
            EnsureResultInstance(info);
            foreach (PropertyPair item in GetPropertyPair(info))
                CopyProperty(info, item);
        }

        private static void EnsureResultInstance(ContextInfo info)
        {
            if (!info.ResultIsValueType)
            {
                Label label = info.ILGen.DefineLabel();
                LoadResult(info);
                info.ILGen.Emit(OpCodes.Brtrue_S, label);
                TryCreateResultInstance(info);
                info.ILGen.MarkLabel(label);
            }
        }

        private static void TryCreateResultInstance(ContextInfo info)
        {
            ConstructorInfo ctor = info.ResultType.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                info.ILGen.Emit(OpCodes.Ret);
            }
            else
            {
                info.ILGen.Emit(OpCodes.Ldarg_2);
                info.ILGen.Emit(OpCodes.Newobj, ctor);
                info.ILGen.Emit(OpCodes.Stind_Ref);
            }
        }

        private static void CopyProperty(ContextInfo info, PropertyPair item)
        {
            Label skip = info.ILGen.DefineLabel();
            LocalBuilder lb = info.ILGen.DeclareLocal(item.ResultProperty.PropertyType);
            if (item.ResultProperty.PropertyType.IsValueType)
            {
                info.ILGen.Emit(OpCodes.Ldloca, lb);
                info.ILGen.Emit(OpCodes.Initobj, item.ResultProperty.PropertyType);
            }
            else
            {
                info.ILGen.Emit(OpCodes.Ldnull);
                info.ILGen.Emit(OpCodes.Stloc, lb);
            }
            LoadResult(info);
            info.ILGen.Emit(OpCodes.Ldarg_0);
            LoadSourceProperty(info, item);
            if (!item.SourceProperty.PropertyType.IsValueType)
            {
                Label notNull = info.ILGen.DefineLabel();
                info.ILGen.Emit(OpCodes.Dup);
                info.ILGen.Emit(OpCodes.Brtrue_S, notNull);
                info.ILGen.Emit(OpCodes.Pop);
                info.ILGen.Emit(OpCodes.Pop);
                info.ILGen.Emit(OpCodes.Pop);
                info.ILGen.Emit(OpCodes.Br_S, skip);
                info.ILGen.MarkLabel(notNull);
            }
            info.ILGen.Emit(OpCodes.Ldloca, lb);
            info.ILGen.Emit(OpCodes.Callvirt, CopyMethod.MakeGenericMethod(item.SourceProperty.PropertyType, item.ResultProperty.PropertyType));
            info.ILGen.Emit(OpCodes.Ldloc, lb);
            SetResultProperty(info, item);
            info.ILGen.MarkLabel(skip);
        }

        protected virtual IEnumerable<PropertyPair> GetPropertyPair(ContextInfo info)
        {
            PropertyInfo[] srcProps = info.SourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] resultProps = info.ResultType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo resultProp in resultProps)
            {
                if (resultProp.GetIndexParameters().Length > 0)
                    continue;
                MethodInfo setMethod = resultProp.GetSetMethod(false);
                if (setMethod == null)
                    continue;
                foreach (PropertyInfo srcProp in srcProps)
                {
                    if (srcProp.Name != resultProp.Name)
                        continue;
                    if (srcProp.GetIndexParameters().Length > 0)
                        continue;
                    if (srcProp.GetGetMethod(false) == null)
                        continue;
                    yield return new PropertyPair(srcProp, resultProp);
                    break;
                }
            }
        }

        private static void LoadResult(ContextInfo info)
        {
            info.ILGen.Emit(OpCodes.Ldarg_2);
            if (!info.ResultIsValueType)
            {
                info.ILGen.Emit(OpCodes.Ldind_Ref);
                info.ILGen.Emit(OpCodes.Castclass, info.ResultType);
            }
        }

        private static void LoadSourceProperty(ContextInfo info, PropertyPair item)
        {
            if (info.SourceIsValueType)
            {
                info.ILGen.Emit(OpCodes.Ldarga_S, 1);
                info.ILGen.Emit(OpCodes.Call, item.SourceProperty.GetGetMethod(false));
            }
            else
            {
                info.ILGen.Emit(OpCodes.Ldarg_1);
                info.ILGen.Emit(OpCodes.Callvirt, item.SourceProperty.GetGetMethod(false));
            }
        }

        private static void SetResultProperty(ContextInfo info, PropertyPair item)
        {
            if (info.ResultIsValueType)
                info.ILGen.Emit(OpCodes.Call, item.ResultProperty.GetSetMethod(false));
            else
                info.ILGen.Emit(OpCodes.Callvirt, item.ResultProperty.GetSetMethod(false));
        }

        #endregion


    }
}