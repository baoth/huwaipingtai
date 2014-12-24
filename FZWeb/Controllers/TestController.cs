using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSmart.Core.DataBase;
using QSmart.Core.Object;
using DataModel;

namespace FZ.Controllers
{
    public class TestController : Controller
    {
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTable() 
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.CreateTable<YanSeZu>(QSmartTableType.InnoDB);
            db.CreateTable<YanSeZuMingXi>(QSmartTableType.InnoDB);
            db.CreateTable<YanSe>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMaZu>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMaZuMingXi>(QSmartTableType.InnoDB);
            db.CreateTable<ChiMa>(QSmartTableType.InnoDB);

            db.CreateTable<PinPai>(QSmartTableType.InnoDB);
            db.CreateTable<ShangPin>(QSmartTableType.InnoDB);
            db.CreateTable<ShangPinFenLei>(QSmartTableType.InnoDB);

            db.CreateTable<JingXiaoShang>(QSmartTableType.InnoDB);
            

            db.CreateTable<CaoZuoYuan>(QSmartTableType.InnoDB);
            db.CreateTable<MenDian>(QSmartTableType.InnoDB);
            db.CreateTable<MenDian_CanZuoYuan_GuanLian>(QSmartTableType.InnoDB);

            db.CreateTable<CangKu>(QSmartTableType.InnoDB);
            db.CreateTable<HuoWei>(QSmartTableType.InnoDB);
            
            db.CreateTable<RuKu>(QSmartTableType.InnoDB);
            db.CreateTable<RuKuMingXi>(QSmartTableType.InnoDB);
            return Content("ok");
        }

        public ActionResult CategoryData()
        {

            
            

            //List<string> level1name = new List<string>{
            //    "服饰内衣","鞋靴","手机","家用电器","数码","电脑、办公","个护化妆",
            //    "图书","母婴","食品饮料","家居家装","汽车用品","礼品箱包","运动户外",
            //    "玩具乐器","钟表","厨具","珠宝首饰","音乐","宠物生活","影视","教育音像",
            //    "家具","家装建材","营养保健","生鲜","酒类","整车","生活旅行","数字娱乐"};

            

            //List<string> level2name10 = new List<string> { "男装", "女装", "内衣", "服饰配件" };
            
            //List<string> level2name11 = new List<string> { "流行男鞋", "时尚女鞋" };
            //List<string> level2name12 = new List<string> { "手机通讯", "手机配件", "运营商" };
            //List<string> level2name13 = new List<string> { "个护健康", "生活电器", "厨房电器", "大 家 电", "五金家装"};
            //List<string> level2name14 = new List<string> { "摄影摄像", "数码配件", "时尚影音", "电子教育", "智能设备"};
            //List<string> level2name15 = new List<string> { "电脑整机", "电脑配件", "外设产品", "网络产品", "办公设备", "文具/耗材" };
            //List<string> level2name16 = new List<string> { "面部护肤", "身体护肤", "口腔护理", "女性护理", "香水彩妆", "洗发护发" };

            cdata(new List<string> { "运动户外" }, 10, "", 1);


            List<string> level2 = new List<string> { "户外装备", "健身训练", "纤体瑜伽", 
                                                           "体育用品", "运动鞋包", "运动服饰",
                                                           "骑行运动", "户外鞋服", "垂钓用品","游泳用品"};
            cdata(level2, 10, "10", 2);
            Dictionary<int, List<string>> level3 = new Dictionary<int, List<string>>()
            {
                {1010,new List<string>{"帐篷/垫子","旅游用品","便携桌椅床","野餐烧烤","军迷用品","救援装备","滑雪装备","睡袋/吊床",
                                       "极限户外","冲浪潜水","登山攀岩","背包","户外配饰","户外照明","户外仪表","户外工具","望远镜"}},
                {1011,new List<string>{"综合训练器","健身车/动感单车","跑步机","运动护具","其他大型器械","其他中小型器材","哑铃","仰卧板/收腹机",
                                       "瑜伽舞蹈","武术搏击"}},
                {1012,new List<string>{"瑜伽垫","瑜伽服","瑜伽配件","瑜伽套装","舞蹈鞋服"}},
                {1013,new List<string>{"羽毛球","乒乓球","篮球","足球","网球","排球","高尔夫","台球",
                                       "棋牌麻将","轮滑滑板","其他"}},
                {1014,new List<string>{"跑步鞋","拖鞋","运动包","休闲鞋","篮球鞋","板鞋","帆布鞋","足球鞋",
                                       "乒羽网鞋","专项运动鞋","训练鞋"}}, 
                {1015,new List<string>{"羽绒服","运动背心","毛衫/线衫","运动配饰","棉服","运动裤","夹克/风衣","卫衣/套头衫",
                                       "T恤","套装","乒羽网服","训练服"}}
            };                    
        }


        private string cdata(List<string> names, int beginbianma, string pbinama,int level)
        {
            try
            {
                QSmartDatabaseClient db = DataBaseProvider.Create("db");
                foreach (string name in names)
                {
                    db.InsertEntity(new ShangPinFenLei
                    {
                        BianMa = pbinama + beginbianma.ToString(),
                        PBianMa = pbinama,
                        Name = name,
                        Level = level
                    }.CreateQSmartObject());
                    beginbianma++;
                }
                db.SaveChange();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "ok";
        }

    }
}
