//using System;
//using System.Collections.ObjectModel;
//using System.Reflection;
//using NHibernate.Hql.Ast;
//using NHibernate.Linq;
//using NHibernate.Linq.Functions;
//using NHibernate.Linq.Visitors;
//using SyncSoft.ROM.Infrastructure.DataNhibernate.Helpers;
//using Expression = System.Linq.Expressions.Expression;

//namespace Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers
//{
//    public class ExtendedToInt32Generator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedToInt32Generator()
//        {
//            // the methods call are used only to get info about the signature, the actual parameter is just ignored
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => Convert.ToInt32(null)) };
//        }

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.MethodCall("Convert.ToInt32", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }
//    }


//    public class ExtendedToInt64Generator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedToInt64Generator()
//        {
//            // the methods call are used only to get info about the signature, the actual parameter is just ignored
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => Convert.ToInt64(null)) };
//        }

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.MethodCall("Convert.ToInt64", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }
//    }

//    public class ExtendedToDateTimeGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedToDateTimeGenerator()
//        {
//            // the methods call are used only to get info about the signature, the actual parameter is just ignored
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => Convert.ToDateTime(null)) };
//        }

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.MethodCall("Convert.ToDateTime", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }
//    }

//    public class ExtendedToOraleCharGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedToOraleCharGenerator()
//        {
//            // the methods call are used only to get info about the signature, the actual parameter is just ignored
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.ToOracleChar(null, null)) };
//        }

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.MethodCall("NhExpansion.ToOracleChar", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }
//    }

//    public class ExtendedIsLikeGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedIsLikeGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.IsLike(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.Like(visitor.Visit(arguments[0]).AsExpression(), visitor.Visit(arguments[1]).AsExpression());
//        }

//        #endregion
//    }

//    public class ExtendedIsBetweenGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedIsBetweenGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.IsBetween(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("NhExpansion.IsBetween", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }

//    public class ExtendedIsAndGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedIsAndGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition<NhExpansion.NhExpansionBetweenBuilder>(x => x.And(null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("And", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }


//    public class ExtendedGreaterthanEqualGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedGreaterthanEqualGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.GreaterthanEqual(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("NhExpansion.GreaterthanEqual", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }


//    public class ExtendedGreaterthanGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedGreaterthanGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.Greaterthan(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("NhExpansion.Greaterthan", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }

//    public class ExtendedLessthanEqualGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedLessthanEqualGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.LessthanEqual(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("NhExpansion.LessthanEqual", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }


//    public class ExtendedLessthanGenerator : BaseHqlGeneratorForMethod
//    {
//        public ExtendedLessthanGenerator()
//        {
//            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhExpansion.Lessthan(null, null)) };
//        }

//        #region Overrides of BaseHqlGeneratorForMethod

//        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
//        {
//            return treeBuilder.BooleanMethodCall("NhExpansion.Lessthan", new[] { 
//                visitor.Visit(arguments[0]).AsExpression(), 
//                visitor.Visit(arguments[1]).AsExpression(), 
//                visitor.Visit(targetObject).AsExpression() 
//            });
//        }

//        #endregion
//    }



//    public sealed class HqlGeneratorForMethodExtend : DefaultLinqToHqlGeneratorsRegistry
//    {
//        public HqlGeneratorForMethodExtend()
//        {
//            this.Merge(new ExtendedToOraleCharGenerator());
//            this.Merge(new ExtendedToInt64Generator());
          
//            this.Merge(new ExtendedToDateTimeGenerator());
//            this.Merge(new ExtendedIsLikeGenerator());
//            this.Merge(new ExtendedIsBetweenGenerator());
//            this.Merge(new ExtendedIsAndGenerator());
//            this.Merge(new ExtendedGreaterthanEqualGenerator());
//            this.Merge(new ExtendedGreaterthanGenerator());
//            this.Merge(new ExtendedLessthanEqualGenerator());
//            this.Merge(new ExtendedLessthanGenerator());

//        }
//    }

 

//}
