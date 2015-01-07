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
            //db.CreateTable<ShangJia_ShangPin_TuCe>(QSmartTableType.InnoDB);
            //db.CreateTable<ShangJia_Sku_Info>(QSmartTableType.InnoDB);
            //db.CreateTable<ShangJia_Sku_TuTou>(QSmartTableType.InnoDB);
            //db.CreateTable<YanSeZu>(QSmartTableType.InnoDB);
            //db.CreateTable<YanSeZuMingXi>(QSmartTableType.InnoDB);
            //db.CreateTable<YanSe>(QSmartTableType.InnoDB);
            //db.CreateTable<ChiMaZu>(QSmartTableType.InnoDB);
            //db.CreateTable<ChiMaZuMingXi>(QSmartTableType.InnoDB);
            //db.CreateTable<ChiMa>(QSmartTableType.InnoDB);

            //db.CreateTable<PinPai>(QSmartTableType.InnoDB);
           db.CreateTable<ShangPin>(QSmartTableType.InnoDB);
            //db.CreateTable<ShangPinFenLei>(QSmartTableType.InnoDB);

            //db.CreateTable<JingXiaoShang>(QSmartTableType.InnoDB);
            

            //db.CreateTable<CaoZuoYuan>(QSmartTableType.InnoDB);
            //db.CreateTable<MenDian>(QSmartTableType.InnoDB);
            ////db.CreateTable<MenDian_CanZuoYuan_GuanLian>(QSmartTableType.InnoDB);

            //db.CreateTable<CangKu>(QSmartTableType.InnoDB);
            //db.CreateTable<HuoWei>(QSmartTableType.InnoDB);
            
            //db.CreateTable<RuKu>(QSmartTableType.InnoDB);
            //db.CreateTable<RuKuMingXi>(QSmartTableType.InnoDB);
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
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            cdata(db,new List<string> { "运动户外" }, 10, "", 1);


            List<string> level2 = new List<string> { "户外装备", "健身训练", "纤体瑜伽", 
                                                           "体育用品", "运动鞋包", "运动服饰",
                                                           "骑行运动", "户外鞋服", "垂钓用品","游泳用品"};
            cdata(db,level2, 10, "10", 2);
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
                                       "T恤","套装","乒羽网服","训练服"}},
                {1016,new List<string>{"折叠车","山地车/公路车","电动车","其他整车","骑行服","骑行装备"}},
                {1017,new List<string>{"软壳衣裤","T恤","户外风衣","功能内衣","军迷服饰","登山鞋","雪地靴","徒步鞋",
                                       "越野跑鞋","休闲鞋","工装鞋","溯溪鞋","沙滩/凉拖","户外袜","冲锋衣裤","速干衣裤","滑雪服","羽绒服/棉服","休闲衣裤","抓绒衣裤"}},
                {1018,new List<string>{"鱼竿鱼线","其它","浮漂鱼饵","钓鱼桌椅","钓鱼配件","钓箱鱼包"}},
                {1019,new List<string>{"泳镜","比基尼","其它","泳帽","游泳包防水包","女士泳衣","男士泳衣"}}
            };
            foreach (int key in level3.Keys)
            {
                cdata(db,level3[key], 10, key.ToString(), 3);
            }
            try
            {
                db.SaveChange();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("ok");
        }


        private void cdata(QSmartDatabaseClient db, List<string> names, int beginbianma, string pbinama,int level)
        {
            
                
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
                
            
        }


        public ActionResult yansechima()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new YanSe { Name = "红色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "黑色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "藏蓝色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "白色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "墨绿色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "天青色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "黄色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "绿色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "草绿色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "紫色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "宝石蓝色" }.CreateQSmartObject());
            db.InsertEntity(new YanSe { Name = "磨砂岩色" }.CreateQSmartObject());

            db.InsertEntity(new ChiMa { Name = "S" }.CreateQSmartObject());
            db.InsertEntity(new ChiMa { Name = "M" }.CreateQSmartObject());
            db.InsertEntity(new ChiMa { Name = "L" }.CreateQSmartObject());
            db.InsertEntity(new ChiMa { Name = "XL" }.CreateQSmartObject());
            db.InsertEntity(new ChiMa { Name = "XXL" }.CreateQSmartObject());
            db.SaveChange();
            return Content("ok");
        }
        /// <summary>
        /// 品牌
        /// </summary>
        /// <returns></returns>
        public ActionResult pinpai()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new PinPai { Name = "丹杰仕",Logo="DANJIESHI" }.CreateQSmartObject());
            db.InsertEntity(new PinPai { Name = "佐丹奴", Logo = "Giordano" }.CreateQSmartObject());
            db.SaveChange();
            return Content("ok");
        }
        /// <summary>
        /// 颜色组 
        /// </summary>
        /// <returns></returns>
        public ActionResult yansezu()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new YanSeZu { Name = "丹杰仕组", PinPaiId = 1 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZu { Name = "佐丹奴组", PinPaiId = 2 }.CreateQSmartObject());
            db.SaveChange();

            return Content("ok");
        }
        /// <summary>
        /// 颜色组明细
        /// </summary>
        /// <returns></returns>
        public ActionResult yansezumingxi()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId=1, YanSeId=1 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 1, YanSeId = 2 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 1, YanSeId = 3 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 1, YanSeId = 4 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 1, YanSeId = 5 }.CreateQSmartObject());

            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 2, YanSeId = 1 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 2, YanSeId = 2 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 2, YanSeId = 3 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 2, YanSeId = 4 }.CreateQSmartObject());
            db.InsertEntity(new YanSeZuMingXi { YanSeZuId = 2, YanSeId = 5 }.CreateQSmartObject());

            db.SaveChange();
            return Content("ok");
        }
       /// <summary>
       /// 尺码组
       /// </summary>
       /// <returns></returns>
        public ActionResult zhimazu()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new ChiMaZu { Name = "丹杰仕尺码组", PinPaiId = 1 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZu { Name = "佐丹奴尺码组", PinPaiId = 2 }.CreateQSmartObject());
            db.SaveChange();

            return Content("ok");
        }
        /// <summary>
        /// 尺码组明细
        /// </summary>
        /// <returns></returns>
        public ActionResult chimazumingxi()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 1, ChiMaId = 1 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 1, ChiMaId = 2 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 1, ChiMaId = 3 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 1, ChiMaId = 4 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 1, ChiMaId = 5 }.CreateQSmartObject());

            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 2, ChiMaId = 1 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 2, ChiMaId = 2 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 2, ChiMaId = 3 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 2, ChiMaId = 4 }.CreateQSmartObject());
            db.InsertEntity(new ChiMaZuMingXi { ChiMaZuId = 2, ChiMaId = 5 }.CreateQSmartObject());

            db.SaveChange();
            return Content("ok");
        }
        /// <summary>
        /// 商品
        /// </summary>
        /// <returns></returns>
        public ActionResult shangpin()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 1, YanSeZuId = 1, PinPaiId = 1, Name = "中长款连帽棉服男装" }.CreateQSmartObject());
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 1, YanSeZuId = 1, PinPaiId = 1, Name = "韩版修身男装棉袄" }.CreateQSmartObject());
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 1, YanSeZuId = 1, PinPaiId = 1, Name = "时尚羊羔绒棉服外套" }.CreateQSmartObject());
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 1, YanSeZuId = 1, PinPaiId = 1, Name = "可脱卸大毛领连帽棉衣" }.CreateQSmartObject());
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 1, YanSeZuId = 1, PinPaiId = 1, Name = "时尚连帽棉服" }.CreateQSmartObject());


            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 2, YanSeZuId = 2, PinPaiId = 2, Name = "保暖情侣潮上衣" }.CreateQSmartObject());
            db.InsertEntity(new ShangPin { ShangPinFenLeiId = 1, ChiMaZuId = 2, YanSeZuId = 2, PinPaiId = 2, Name = "男士长袖POLO衣" }.CreateQSmartObject());


            db.SaveChange();
            return Content("ok");
        }
        /// <summary>
        /// 经销商
        /// </summary>
        /// <returns></returns>
        public ActionResult jingxiaoshang()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new JingXiaoShang { Name = "北京服装经销商",Address="北京 海淀",Phone="010-666666" }.CreateQSmartObject());
            
            db.SaveChange();
            return Content("ok");
        }
        /// <summary>
        /// 门店
        /// </summary>
        /// <returns></returns>
        public ActionResult mendian()
        {
            QSmartDatabaseClient db = DataBaseProvider.Create("db");
            db.InsertEntity(new MenDian { Name = "北京大红门服装店", JingXiaoShangId=1, Address = "北京", Phone = "010-666666" }.CreateQSmartObject());
            db.InsertEntity(new MenDian { Name = "北京西单服装店", JingXiaoShangId = 1, Address = "北京", Phone = "010-666666" }.CreateQSmartObject());
            db.SaveChange();
            return Content("ok");
        }

    }
}
