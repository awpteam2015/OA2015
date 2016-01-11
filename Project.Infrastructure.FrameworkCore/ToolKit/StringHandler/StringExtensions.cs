using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.ToolKit.StringHandler
{
    /// <summary>
    ///  对字符串 String 的扩展操作
    /// </summary>
    public static class StringExtensions
    {
        #region 对字符串 String 的扩展

        #region string转stream
        /// <summary>
        /// string转stream
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Stream ToStream(this string value)
        {
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(value));
            return stream;
        }
        #endregion



        #region GetLength 获取字符串的字节长度
        /// <summary>
        /// 获取字符串的字节长度
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>字符串的字节长度</returns>
        public static int GetLength(this string s)
        {
            return s.GetLength(Encoding.UTF8);
        }

        public static int GetLength(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s).Length;
        }
        #endregion

        #region CutString 截取指定长度字符 （二分法）
        /// <summary>
        /// 截取指定长度字符
        /// 1. 如果isChWord为false, 把单个字符都看成一个长度。结果就是截取ostr的头len个字符返回
        /// 如：CutString("1234567", 2, false) 返回'12'
        ///     CutString("一234567", 2, false) 返回'一2'
        ///     CutString("一而似利古", 2, false) 返回'一而'
        /// 2. 如果isChWord为true,把一个单字节字符都看成一个长度,把双字节字符（如中文字符，韩文，日文）看成两个字符，len表示的是双字节个数。
        /// 如：CutString("1234567", 2, true) 返回'1234'
        ///     CutString("一234567", 2, false) 返回'一23'
        ///     CutString("一而似利古", 2, false) 返回'一而'
        /// </summary>
        /// <param name="ostr">原字符串</param>
        /// <param name="len">截取长度</param>
        /// <param name="isChWord">是否中文长度为准</param>
        /// <returns>
        /// 截取的指定长度字符
        /// </returns>
        /// <remarks>
        /// 在截取指定长度字符时，我们可能会遇到单字节字符和双字节字符混和的情况（如中英文混合），
        /// 我们如果用一个字符占一个长度的方法计算要截取的字符串长度，对不同字符串截取，得到的长度可能不一样
        /// 如：CutLenStr("1234567", 2, false)    返回'12'
        ///     CutLenStr("一而似利古", 2, false) 返回'一而'。
        /// 为解决这个问题，我们在计算字符串长度时就一该以字节长度为准。下面的方法就是以字节长度为准计算字符串长度的，采用的是二分法算法。
        /// </remarks>
        public static string CutString(this string ostr, int len, bool isChWord)
        {
            ostr = System.Text.RegularExpressions.Regex.Replace(ostr, "<[^>]+>", ""); //过滤掉字符串中的html代码
            if (isChWord == false)//如果isChWord为false, 把所有单个字符都看成一个长度
            {
                return ostr.Length > len ? ostr.Substring(0, len) : ostr;
            }
            else //如果isChWord为true,把一个单字节字符都看成一个长度,把双字节字符（如中文字符，韩文，日文）看成两个字符
            {
                if (ostr.Length == 1 && len > 0)
                    return ostr;
                if (len == 1 && ostr.Length > 0)
                    return ostr.Substring(0, 1);
                int lenc = len * 2;
                int ostrLen = ostr.GetLength();
                if (ostrLen <= lenc)
                    return ostr;

                return GetLenStr(ostr, ref ostr, lenc);

            }
        }

        private static string GetLenStr(string str, ref string ostr, int len)
        {
            int totalLen = str.GetLength();
            if (totalLen == len)
                return str;

            if (totalLen + 1 == len)
            {
                string astr = ostr.Substring(0, str.Length + 1);
                if (astr.GetLength() == len)
                {
                    return astr;
                }
                return str;
            }


            if (totalLen - 1 == len)
            {
                string dstr = ostr.Substring(0, str.Length - 1);
                if (dstr.GetLength() <= len)
                {
                    return dstr;
                }
                return str;
            }

            if (totalLen < len)
            {
                int temp = str.Length + Convert.ToInt32((len - totalLen) * 0.5);
                str = ostr.Substring(0, temp);
                return GetLenStr(str, ref ostr, len);
            }
            else
            {
                int temp = Convert.ToInt32((totalLen - len) * 0.5);
                str = ostr.Substring(0, temp);
                return GetLenStr(str, ref ostr, len);
            }
        }

        #endregion

        #region ToSBC, ToDBC 转全角半角
        /**/
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>        
        public static string ToSBC(this string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /**/
        /// <summary>
        /// 转半角的函数(DBC case) ,一般用于如：密码等的转换，方便用户在任何状态下输入一致的信息
        /// 我们在做程序的的时候经常要处理用户输入，作为我们的主要语言中文，经常会出现全角、半角的问题，这会在查询时给我们带来很多麻烦。
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        /// <summary>
        /// 去除原字符串结尾处的所有替换字符串
        /// 如：原字符串"sdlfjdcdcd",替换字符串"cd" 返回"sdlfjd"
        /// </summary>
        /// <param name="strSrc"></param>
        /// <param name="strTrim"></param>
        /// <returns></returns>
        public static string TrimEnd(this string strSrc, string strTrim)
        {
            if (strSrc.EndsWith(strTrim))
            {
                string strDes = strSrc.Substring(0, strSrc.Length - strTrim.Length);
                return TrimEnd(strDes, strTrim);
            }
            return strSrc;
        }

        #region 获取字符串符合正则表达式的字符串内容
        private static readonly Regex MergeBlankRegex = new Regex(@"\s+");
        /// <summary>
        /// 去除连续空格(多个空格合并为一个)，把所有空格变为一个空格
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string MergeBlank(this string strInput)
        {
            return MergeBlankRegex.Replace(strInput, " ").Trim();
        }

        /// <summary>
        /// 去除空格
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string RemoveBlank(this string strInput)
        {
            return MergeBlankRegex.Replace(strInput, "").Trim();
        }

        /// <summary>
        /// 获取字符串符合正则表达式的字符串内容
        /// string s = "ldp615".Match("[a-zA-Z]+");
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }
        #endregion

        #region 检测字符串的长度是否在2数字之间
        /// <summary>
        /// 检测字符串的长度是否在2数字之间
        /// </summary>
        /// <param name="str">要检测的字符串</param>
        /// <param name="min">最短必须多少</param>
        /// <param name="max">最长必须多少</param>
        /// <returns></returns>
        public static bool IsBetween(this string str, int min, int max)
        {
            int length = string.IsNullOrEmpty(str) ? 0 : str.Length;
            return (length >= min) && (length <= max);
        }
        #endregion

        #region Trans:繁简互转
        /// <summary>
        /// 繁简互转
        /// string txt = "要处理的内容";
        /// response.write(txt.Trans(true));
        /// </summary>
        /// <param name="content">要处理的内容</param>
        /// <param name="isToSimple">true:转为简体 false:转为繁体</param>
        /// <returns>要处理的内容</returns>
        public static string Trans(this string content, bool isToSimple)
        {
            //注意：简体有些字没有，仍用繁体字。如下：“琺”
            //新加的字都放在前面
            string complex = "槓穌靨嘆繽軼隻製嚀幺皚藹礙愛翺襖奧壩罷擺敗頒辦絆幫綁鎊謗剝飽寶報鮑輩貝鋇狽備憊繃筆畢斃幣閉邊編貶變辯辮標鼈別癟瀕濱賓擯餅並撥缽鉑駁蔔補財參蠶殘慚慘燦蒼艙倉滄廁側冊測層詫攙摻蟬饞讒纏鏟産闡顫場嘗長償腸廠暢鈔車徹塵沈陳襯撐稱懲誠騁癡遲馳恥齒熾沖蟲寵疇躊籌綢醜櫥廚鋤雛礎儲觸處傳瘡闖創錘純綽辭詞賜聰蔥囪從叢湊躥竄錯達帶貸擔單鄲撣膽憚誕彈當擋黨蕩檔搗島禱導盜燈鄧敵滌遞締顛點墊電澱釣調叠諜疊釘頂錠訂丟東動棟凍鬥犢獨讀賭鍍鍛斷緞兌隊對噸頓鈍奪墮鵝額訛惡餓兒爾餌貳發罰閥琺礬釩煩範販飯訪紡飛誹廢費紛墳奮憤糞豐楓鋒風瘋馮縫諷鳳膚輻撫輔賦複負訃婦縛該鈣蓋幹趕稈贛岡剛鋼綱崗臯鎬擱鴿閣鉻個給龔宮鞏貢鈎溝構購夠蠱顧剮關觀館慣貫廣規矽歸龜閨軌詭櫃貴劊輥滾鍋國過駭韓漢號閡鶴賀橫轟鴻紅後壺護滬戶嘩華畫劃話懷壞歡環還緩換喚瘓煥渙黃謊揮輝毀賄穢會燴彙諱誨繪葷渾夥獲貨禍擊機積饑譏雞績緝極輯級擠幾薊劑濟計記際繼紀夾莢頰賈鉀價駕殲監堅箋間艱緘繭檢堿鹼揀撿簡儉減薦檻鑒踐賤見鍵艦劍餞漸濺澗將漿蔣槳獎講醬膠澆驕嬌攪鉸矯僥腳餃繳絞轎較稭階節莖鯨驚經頸靜鏡徑痙競淨糾廄舊駒舉據鋸懼劇鵑絹傑潔結誡屆緊錦僅謹進晉燼盡勁荊覺決訣絕鈞軍駿開凱顆殼課墾懇摳庫褲誇塊儈寬礦曠況虧巋窺饋潰擴闊蠟臘萊來賴藍欄攔籃闌蘭瀾讕攬覽懶纜爛濫撈勞澇樂鐳壘類淚籬離裏鯉禮麗厲勵礫曆瀝隸倆聯蓮連鐮憐漣簾斂臉鏈戀煉練糧涼兩輛諒療遼鐐獵臨鄰鱗凜賃齡鈴淩靈嶺領餾劉龍聾嚨籠壟攏隴樓婁摟簍蘆盧顱廬爐擄鹵虜魯賂祿錄陸驢呂鋁侶屢縷慮濾綠巒攣孿灤亂掄輪倫侖淪綸論蘿羅邏鑼籮騾駱絡媽瑪碼螞馬罵嗎買麥賣邁脈瞞饅蠻滿謾貓錨鉚貿麽黴沒鎂門悶們錳夢謎彌覓冪綿緬廟滅憫閩鳴銘謬謀畝鈉納難撓腦惱鬧餒內擬膩攆撚釀鳥聶齧鑷鎳檸獰甯擰濘鈕紐膿濃農瘧諾歐鷗毆嘔漚盤龐賠噴鵬騙飄頻貧蘋憑評潑頗撲鋪樸譜棲淒臍齊騎豈啓氣棄訖牽扡釺鉛遷簽謙錢鉗潛淺譴塹槍嗆牆薔強搶鍬橋喬僑翹竅竊欽親寢輕氫傾頃請慶瓊窮趨區軀驅齲顴權勸卻鵲確讓饒擾繞熱韌認紉榮絨軟銳閏潤灑薩鰓賽叁傘喪騷掃澀殺紗篩曬刪閃陝贍繕傷賞燒紹賒攝懾設紳審嬸腎滲聲繩勝聖師獅濕詩屍時蝕實識駛勢適釋飾視試壽獸樞輸書贖屬術樹豎數帥雙誰稅順說碩爍絲飼聳慫頌訟誦擻蘇訴肅雖隨綏歲孫損筍縮瑣鎖獺撻擡態攤貪癱灘壇譚談歎湯燙濤縧討騰謄銻題體屜條貼鐵廳聽烴銅統頭禿圖塗團頹蛻脫鴕馱駝橢窪襪彎灣頑萬網韋違圍爲濰維葦偉僞緯謂衛溫聞紋穩問甕撾蝸渦窩臥嗚鎢烏汙誣無蕪吳塢霧務誤錫犧襲習銑戲細蝦轄峽俠狹廈嚇鍁鮮纖鹹賢銜閑顯險現獻縣餡羨憲線廂鑲鄉詳響項蕭囂銷曉嘯蠍協挾攜脅諧寫瀉謝鋅釁興洶鏽繡虛噓須許敘緒續軒懸選癬絢學勳詢尋馴訓訊遜壓鴉鴨啞亞訝閹煙鹽嚴顔閻豔厭硯彥諺驗鴦楊揚瘍陽癢養樣瑤搖堯遙窯謠藥爺頁業葉醫銥頤遺儀彜蟻藝億憶義詣議誼譯異繹蔭陰銀飲隱櫻嬰鷹應纓瑩螢營熒蠅贏穎喲擁傭癰踴詠湧優憂郵鈾猶遊誘輿魚漁娛與嶼語籲禦獄譽預馭鴛淵轅園員圓緣遠願約躍鑰嶽粵悅閱雲鄖勻隕運蘊醞暈韻雜災載攢暫贊贓髒鑿棗竈責擇則澤賊贈紮劄軋鍘閘柵詐齋債氈盞斬輾嶄棧戰綻張漲帳賬脹趙蟄轍鍺這貞針偵診鎮陣掙睜猙爭幀鄭證織職執紙摯擲幟質滯鍾終種腫衆謅軸皺晝驟豬諸誅燭矚囑貯鑄築駐專磚轉賺樁莊裝妝壯狀錐贅墜綴諄著濁茲資漬蹤綜總縱鄒詛組鑽為麼於產崙眾餘衝準兇佔歷釐髮臺嚮啟週譁薑寧傢尷鉅乾倖徵逕誌愴恆託摺掛闆樺慾洩瀏薰箏籤蹧係紓燿骼臟捨甦盪穫讚輒蹟跡採裡鐘鏢閒闕僱靂獃騃佈牀脣閧鬨崑崐綑蔴阩昇牠蓆巖灾剳紥註";
            string simple = "杠稣厣叹缤轶只制咛么皑蔼碍爱翱袄奥坝罢摆败颁办绊帮绑镑谤剥饱宝报鲍辈贝钡狈备惫绷笔毕毙币闭边编贬变辩辫标鳖别瘪濒滨宾摈饼并拨钵铂驳卜补财参蚕残惭惨灿苍舱仓沧厕侧册测层诧搀掺蝉馋谗缠铲产阐颤场尝长偿肠厂畅钞车彻尘沉陈衬撑称惩诚骋痴迟驰耻齿炽冲虫宠畴踌筹绸丑橱厨锄雏础储触处传疮闯创锤纯绰辞词赐聪葱囱从丛凑蹿窜错达带贷担单郸掸胆惮诞弹当挡党荡档捣岛祷导盗灯邓敌涤递缔颠点垫电淀钓调迭谍叠钉顶锭订丢东动栋冻斗犊独读赌镀锻断缎兑队对吨顿钝夺堕鹅额讹恶饿儿尔饵贰发罚阀琺矾钒烦范贩饭访纺飞诽废费纷坟奋愤粪丰枫锋风疯冯缝讽凤肤辐抚辅赋复负讣妇缚该钙盖干赶秆赣冈刚钢纲岗皋镐搁鸽阁铬个给龚宫巩贡钩沟构购够蛊顾剐关观馆惯贯广规硅归龟闺轨诡柜贵刽辊滚锅国过骇韩汉号阂鹤贺横轰鸿红后壶护沪户哗华画划话怀坏欢环还缓换唤痪焕涣黄谎挥辉毁贿秽会烩汇讳诲绘荤浑伙获货祸击机积饥讥鸡绩缉极辑级挤几蓟剂济计记际继纪夹荚颊贾钾价驾歼监坚笺间艰缄茧检碱硷拣捡简俭减荐槛鉴践贱见键舰剑饯渐溅涧将浆蒋桨奖讲酱胶浇骄娇搅铰矫侥脚饺缴绞轿较秸阶节茎鲸惊经颈静镜径痉竞净纠厩旧驹举据锯惧剧鹃绢杰洁结诫届紧锦仅谨进晋烬尽劲荆觉决诀绝钧军骏开凯颗壳课垦恳抠库裤夸块侩宽矿旷况亏岿窥馈溃扩阔蜡腊莱来赖蓝栏拦篮阑兰澜谰揽览懒缆烂滥捞劳涝乐镭垒类泪篱离里鲤礼丽厉励砾历沥隶俩联莲连镰怜涟帘敛脸链恋炼练粮凉两辆谅疗辽镣猎临邻鳞凛赁龄铃凌灵岭领馏刘龙聋咙笼垄拢陇楼娄搂篓芦卢颅庐炉掳卤虏鲁赂禄录陆驴吕铝侣屡缕虑滤绿峦挛孪滦乱抡轮伦仑沦纶论萝罗逻锣箩骡骆络妈玛码蚂马骂吗买麦卖迈脉瞒馒蛮满谩猫锚铆贸么霉没镁门闷们锰梦谜弥觅幂绵缅庙灭悯闽鸣铭谬谋亩钠纳难挠脑恼闹馁内拟腻撵捻酿鸟聂啮镊镍柠狞宁拧泞钮纽脓浓农疟诺欧鸥殴呕沤盘庞赔喷鹏骗飘频贫苹凭评泼颇扑铺朴谱栖凄脐齐骑岂启气弃讫牵扦钎铅迁签谦钱钳潜浅谴堑枪呛墙蔷强抢锹桥乔侨翘窍窃钦亲寝轻氢倾顷请庆琼穷趋区躯驱龋颧权劝却鹊确让饶扰绕热韧认纫荣绒软锐闰润洒萨鳃赛三伞丧骚扫涩杀纱筛晒删闪陕赡缮伤赏烧绍赊摄慑设绅审婶肾渗声绳胜圣师狮湿诗尸时蚀实识驶势适释饰视试寿兽枢输书赎属术树竖数帅双谁税顺说硕烁丝饲耸怂颂讼诵擞苏诉肃虽随绥岁孙损笋缩琐锁獭挞抬态摊贪瘫滩坛谭谈叹汤烫涛绦讨腾誊锑题体屉条贴铁厅听烃铜统头秃图涂团颓蜕脱鸵驮驼椭洼袜弯湾顽万网韦违围为潍维苇伟伪纬谓卫温闻纹稳问瓮挝蜗涡窝卧呜钨乌污诬无芜吴坞雾务误锡牺袭习铣戏细虾辖峡侠狭厦吓锨鲜纤咸贤衔闲显险现献县馅羡宪线厢镶乡详响项萧嚣销晓啸蝎协挟携胁谐写泻谢锌衅兴汹锈绣虚嘘须许叙绪续轩悬选癣绚学勋询寻驯训讯逊压鸦鸭哑亚讶阉烟盐严颜阎艳厌砚彦谚验鸯杨扬疡阳痒养样瑶摇尧遥窑谣药爷页业叶医铱颐遗仪彝蚁艺亿忆义诣议谊译异绎荫阴银饮隐樱婴鹰应缨莹萤营荧蝇赢颖哟拥佣痈踊咏涌优忧邮铀犹游诱舆鱼渔娱与屿语吁御狱誉预驭鸳渊辕园员圆缘远愿约跃钥岳粤悦阅云郧匀陨运蕴酝晕韵杂灾载攒暂赞赃脏凿枣灶责择则泽贼赠扎札轧铡闸栅诈斋债毡盏斩辗崭栈战绽张涨帐账胀赵蛰辙锗这贞针侦诊镇阵挣睁狰争帧郑证织职执纸挚掷帜质滞钟终种肿众诌轴皱昼骤猪诸诛烛瞩嘱贮铸筑驻专砖转赚桩庄装妆壮状锥赘坠缀谆着浊兹资渍踪综总纵邹诅组钻为么于产仑众余冲准凶占历厘发台向启周哗姜宁家尴巨干幸征径志怆恒托折挂板桦欲泄浏熏筝签糟系纾耀胳脏舍苏荡获赞辄迹迹采里钟镖闲阙雇雳呆呆布床唇哄哄昆昆捆麻升升它席岩灾札扎注";
            string str = "";
            if (isToSimple)
            {
                for (int i = 0; i < content.Length; i++)
                {
                    string word = content.Substring(i, 1);
                    //忽略字母
                    if (string.CompareOrdinal(word, "~") <= 0)
                    {
                        str += word;
                        continue;
                    }
                    int pos = complex.IndexOf(word);
                    if (pos != -1)
                        str += simple.Substring(pos, 1);
                    else
                        str += word;
                }
            }
            else
            {
                for (int i = 0; i < content.Length; i++)
                {
                    string word = content.Substring(i, 1);
                    //忽略字母
                    if (string.CompareOrdinal(word, "~") <= 0)
                    {
                        str += word;
                        continue;
                    }
                    int pos = simple.IndexOf(word);
                    if (pos != -1)
                        str += complex.Substring(pos, 1);
                    else
                        str += word;
                }
            }
            return str;
        }
        #endregion

        #region IsNumber：判断字符串是否为整型
        /// <summary>
        /// 判断字符串是否为整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            int i;
            for (i = 0; i < s.Length; i++)
            {
                char c;
                c = Convert.ToChar(s.Substring(i, 1));

                if (!(c >= '0' && c <= '9'))
                {
                    break;
                }
            }
            if (i == s.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 需要引用 Microsoft.VisualBasic
        ///// <summary>
        ///// 转换为半角字符串
        ///// </summary>
        //public static string 转换为半角(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.Narrow, 0);
        //}

        ///// <summary>
        ///// 转换为简体中文
        ///// </summary>
        //public static string 转换为简体中文(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.SimplifiedChinese, 0);
        //}

        ///// <summary>
        ///// 转换为繁体中文
        ///// </summary>
        //public static string 转换为繁体中文(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.TraditionalChinese, 0);
        //}
        #endregion

        public static string GetStringSpellCode(this string CNStr)
        {
            StringBuilder result = new StringBuilder();//使用StringBuilder优化字符串连接  
            char[] arrChar = CNStr.ToCharArray();
            for (int j = 0; j < arrChar.Length; j++)   //遍历输入的字符   
            {
                result.Append(GetCharSpellCode(arrChar[j].ToString()));
            }
            return result.ToString();
        }

        ///   <summary>
        ///   得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        ///   </summary>
        ///   <param   name="CnChar">单个汉字</param>
        ///   <returns>单个大写字母</returns>
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                //   get   the     array   of   byte   from   the   single   char   
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            #region table   of   the   constant   list
            //expresstion
            //table   of   the   constant   list
            // 'A';           //45217..45252
            // 'B';           //45253..45760
            // 'C';           //45761..46317
            // 'D';           //46318..46825
            // 'E';           //46826..47009
            // 'F';           //47010..47296
            // 'G';           //47297..47613

            // 'H';           //47614..48118
            // 'J';           //48119..49061
            // 'K';           //49062..49323
            // 'L';           //49324..49895
            // 'M';           //49896..50370
            // 'N';           //50371..50613
            // 'O';           //50614..50621
            // 'P';           //50622..50905
            // 'Q';           //50906..51386

            // 'R';           //51387..51445
            // 'S';           //51446..52217
            // 'T';           //52218..52697
            //没有U,V
            // 'W';           //52698..52979
            // 'X';           //52980..53640
            // 'Y';           //53689..54480
            // 'Z';           //54481..55289
            #endregion
            //   iCnChar match     the   constant
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= .51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }
        #endregion

        #region 泛型 扩展
        #region 判断是否在某个集合中
        /// <summary>
        /// 判断是否在某个集合中
        /// bool exist1=  2.In(1, 2, 3);
        /// string[] helloworld = { "Hello", "World", "!" };
        /// bool exist2 = "Hello".In(helloworld);
        /// <author>
        ///        <name>李伟伟</name>
        ///        <date>2009.09.30</date>
        /// </author> 
        /// </summary> 
        /// <param name="t">泛型实体</param>
        /// <param name="c">数组</param>
        /// <returns>bool</returns>
        public static bool In<T>(this T t, params T[] c)
        {
            return c.Contains(t);
            //return c.Any(i => i.Equals(t));
        }
        #endregion

        #region InRange 判断一个元素是否在某个范围
        /// <summary>
        /// 判断一个元素是否在某个范围
        /// 判断3是否在2～3的范围
        /// bool result1 = 3.InRange(2, 3);
        /// 
        /// 判断3.14是否在3.13～3.15的范围
        /// bool result2 = 3.14.InRange(3.13, 3.15);
        /// 
        /// 判断今天是否在2000年1月1日～2010年1月1日的范围
        /// bool result3 = DateTime.Now.InRange(new DateTime(2000, 1, 1), new DateTime(2010, 1, 1));
        /// 
        /// 判断牛B是否在牛A和牛C之间
        /// bool result4 = "牛B".InRange("牛A", "牛C");
        /// <author>
        ///        <name>李伟伟</name>
        ///        <date>2009.09.30</date>
        /// </author> 
        /// </summary> 
        /// <param name="t">泛型实体</param>
        /// <param name="minT">起始值</param>
        /// <param name="minT">结束值</param>
        /// <returns>bool</returns>
        public static bool InRange<T>(this IComparable<T> t, T minT, T maxT)
        {
            return t.CompareTo(minT) >= 0 && t.CompareTo(maxT) <= 0;
        }

        /// <summary>
        /// 判断一个元素是否在某个范围
        /// 判断3是否在2～3的范围
        /// bool result1 = 3.InRange(2, 3);
        /// 
        /// 判断3.14是否在3.13～3.15的范围
        /// bool result2 = 3.14.InRange(3.13, 3.15);
        /// 
        /// 判断今天是否在2000年1月1日～2010年1月1日的范围
        /// bool result3 = DateTime.Now.InRange(new DateTime(2000, 1, 1), new DateTime(2010, 1, 1));
        /// 
        /// 判断牛B是否在牛A和牛C之间
        /// bool result4 = "牛B".InRange("牛A", "牛C");
        /// <author>
        ///        <name>李伟伟</name>
        ///        <date>2009.09.30</date>
        /// </author> 
        /// </summary> 
        /// <param name="t">泛型实体</param>
        /// <param name="minT">起始值</param>
        /// <param name="minT">结束值</param>
        /// <returns>bool</returns>
        public static bool InRange(this IComparable t, object minT, object maxT)
        {
            return t.CompareTo(minT) >= 0 && t.CompareTo(maxT) <= 0;
        }
        #endregion

        #region ForEach 遍历集合
        /// <summary>
        /// ForEach 遍历集合
        /// entities.ForEach<ObjectDefinition>(o => o.DeleteDbRecord());
        /// <author>
        ///        <name>李伟伟</name>
        ///        <date>2009.09.30</date>
        /// </author> 
        /// </summary> 
        /// <param name="source">this IEnumerable<T></param>
        /// <param name="action">委托的方法</param>
        //public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        //{
        //    foreach (T element in source)
        //        action(element);
        //}

        /// <summary>
        /// ForEach 遍历集合
        /// entities.ForEach<ObjectDefinition>(o => o.DeleteDbRecord());
        /// <author>
        ///        <name>李伟伟</name>
        ///        <date>2009.09.30</date>
        /// </author> 
        /// </summary> 
        /// <param name="source">this IEnumerable<T></param>
        /// <param name="action">委托的方法</param>
        //public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        //{
        //    int i = 0;
        //    foreach (T element in source)
        //        action(element, i++);
        //}

        ///// <summary> 
        ///// 可以半途中断执行的遍历方法。 
        ///// </summary> 
        //public static void ForEach<T>(this IEnumerable<T> enumerable, Func<T, bool> handler)
        //{
        //    foreach (var item in enumerable)
        //        if (!handler(item)) break;
        //}

        ///// <summary> 
        ///// 可以半途中段的带索引的遍历方法。 
        ///// </summary> 
        //public static void ForEach<T>(this IEnumerable<T> enumerable, Func<T, int, bool> handler)
        //{
        //    int index = 0;
        //    foreach (var item in enumerable)
        //        if (!handler(item, index++)) break;
        //}
        #endregion

        #region Join 把集合转成以某个特殊字符 串联而成的字符串
        //public static string Join<T>(this IEnumerable<T> source, string separator)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    foreach (T element in source)
        //    {
        //        builder.Append(element + separator);
        //    }
        //    return builder.ToString();
        //}

        ///// <summary> Join the enumerable to a string.</summary>
        //public static string Join<T>(this IEnumerable<T> enumerable, string start, string end, string seperator)
        //{
        //    return Join<T>(enumerable, start, end, seperator, e => e);
        //}

        /// <summary> Join the enumerable to a string.</summary>
        //public static string Join<T>(this IEnumerable<T> enumerable, string start, string end, string seperator, Func<T, object> converter)
        //{

        //    //Expect.ArgumentNotNull(converter, "converter");

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(start);

        //    enumerable.ForEach((e, i) => sb.AppendFormat("{0}{1}", i == 0 ? string.Empty : seperator, converter(e)));

        //    sb.Append(end);

        //    return sb.ToString();
        //}
        #endregion
        #endregion

    }
}
