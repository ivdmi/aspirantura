using System;
using System.Linq;
using AspiranturaSqlite.Models;

namespace AspiranturaSqlite.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AspiranturaContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Aspirants.Any())
            {
                return;   // DB has been seeded
            }

            StatusTypeInit(context);
            OrderTypeInit(context);
            KnowledgeInit(context);
            SpecialityInit(context);
            OrderInit(context);
            AspirantInit(context);
        }
        
        private static void StatusTypeInit(AspiranturaContext context)
        {
            var statuses = new StatusType[]
            {
                new StatusType{Id=(int)StatusTypeEnum.новий, Name=StatusTypeEnum.новий.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.навчання, Name=StatusTypeEnum.навчання.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.академ_відпустка, Name=StatusTypeEnum.академ_відпустка.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.відрахований_2, Name=StatusTypeEnum.відрахований_2.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.захист_достроково, Name=StatusTypeEnum.захист_достроково.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.закінчив, Name=StatusTypeEnum.закінчив.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.повторний_курс, Name=StatusTypeEnum.повторний_курс.ToString()},
                new StatusType{Id=(int)StatusTypeEnum.стажування, Name=StatusTypeEnum.стажування.ToString()}
            };
            foreach (var k in statuses)
            {
                context.Statuses.Add(k);
            }
        }

        private static void OrderTypeInit(AspiranturaContext context)
        {
            var orderTypes = new OrderType[]
            {
                new OrderType{Id=(int)OrderTypeEnum.Зарахування, Name=OrderTypeEnum.Зарахування.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Переведення, Name=OrderTypeEnum.Переведення.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Закінчення, Name=OrderTypeEnum.Закінчення.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Відрахування, Name=OrderTypeEnum.Відрахування.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Академвідпустка, Name=OrderTypeEnum.Академвідпустка.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Інший, Name=OrderTypeEnum.Інший.ToString()},
                new OrderType{Id=(int)OrderTypeEnum.Зміна_форми_навчання, Name=OrderTypeEnum.Зміна_форми_навчання.ToString()}
            };
            foreach (var k in orderTypes)
            {
                context.Ordertypes.Add(k);
            }
            context.SaveChanges();
        }

        private static void KnowledgeInit(AspiranturaContext context)
        {

            var knowledges = new Knowledge[]
            {
                new Knowledge{Id=1, Name="Освіта/Педагогіка"},
                new Knowledge{Id=2, Name="Культура і мистецтво"},
                new Knowledge{Id=3, Name="Гуманітарні науки"},
                new Knowledge{Id=4, Name="Богослов’я"},
                new Knowledge{Id=5, Name="Соціальні та поведінкові науки"},
                new Knowledge{Id=6, Name="Журналістика"},
                new Knowledge{Id=7, Name="Управління та адміністрування"},
                new Knowledge{Id=8, Name="Право"},
                new Knowledge{Id=9, Name="Біологія"},
                new Knowledge{Id=10, Name="Природничі науки"},
                new Knowledge{Id=11, Name="Математика та статистика"},
                new Knowledge{Id=12, Name="Інформаційні технології"},
                new Knowledge{Id=13, Name="Механічна інженерія"},
                new Knowledge{Id=14, Name="Електрична інженерія"},
                new Knowledge{Id=15, Name="Автоматизація та приладобудування"},
                new Knowledge{Id=16, Name="Хімічна та біоінженерія"},
                new Knowledge{Id=17, Name="Електроніка та телекомунікації"},
                new Knowledge{Id=18, Name="Виробництво та технології"},
                new Knowledge{Id=19, Name="Архітектура та будівництво"},
                new Knowledge{Id=20, Name="Аграрні науки та продовольство"},
                new Knowledge{Id=21, Name="Ветеринарна медицина"},
                new Knowledge{Id=22, Name="Охорона здоров’я"},
                new Knowledge{Id=23, Name="Соціальна робота"},
                new Knowledge{Id=24, Name="Сфера обслуговування"},
                new Knowledge{Id=25, Name="Воєнні науки, національна безпека, безпека державного кордону"},
                new Knowledge{Id=26, Name="Цивільна безпека"},
                new Knowledge{Id=27, Name="Транспорт"},
                new Knowledge{Id=28, Name="Публічне управління та адміністрування"},
                new Knowledge{Id=29, Name="Міжнародні відносини"}
            };
            foreach (var k in knowledges)
            {
                context.Knowledges.Add(k);
            }
            context.SaveChanges();
        }

        private static void SpecialityInit(AspiranturaContext context)
        {

            var specialities = new Speciality[]
            {
                new Speciality{Id=11,Name="Освітні, педагогічні науки",KnowledgeId=1},
                new Speciality{Id=12,Name="Дошкільна освіта",KnowledgeId=1},
                new Speciality{Id=13,Name="Початкова освіта",KnowledgeId=1},
                new Speciality{Id=14,Name="Середня освіта (за предметними спеціальностями)",KnowledgeId=1},
                new Speciality{Id=15,Name="Професійна освіта (за спеціалізаціями)",KnowledgeId=1},
                new Speciality{Id=16,Name="Спеціальна освіта",KnowledgeId=1},
                new Speciality{Id=17,Name="Фізична культура і спорт",KnowledgeId=1},
                new Speciality{Id=21,Name="Аудіовізуальне мистецтво та виробництво",KnowledgeId=2},
                new Speciality{Id=22,Name="Дизайн",KnowledgeId=2},
                new Speciality{Id=23,Name="Образотворче мистецтво, декоративне мистецтво, реставрація ",KnowledgeId=2},
                new Speciality{Id=24,Name="Хореографія",KnowledgeId=2},
                new Speciality{Id=25,Name="Музичне мистецтво",KnowledgeId=2},
                new Speciality{Id=26,Name="Сценічне мистецтво",KnowledgeId=2},
                new Speciality{Id=27,Name="Музеєзнавство, пам’яткознавство",KnowledgeId=2},
                new Speciality{Id=28,Name="Менеджмент соціокультурної діяльності",KnowledgeId=2},
                new Speciality{Id=29,Name="інформаційна, бібліотечна та архівна справа",KnowledgeId=2},
                new Speciality{Id=31,Name="Релігієзнавство",KnowledgeId=3},
                new Speciality{Id=32,Name="історія та археологія",KnowledgeId=3,IsUsed=true},
                new Speciality{Id=33,Name="Філософія",KnowledgeId=3,IsUsed=true},
                new Speciality{Id=34,Name="Культурологія",KnowledgeId=3},new Speciality{Id=35,Name="Філологія",KnowledgeId=3,IsUsed=true},
                new Speciality{Id=41,Name="Богослов’я",KnowledgeId=4},
                new Speciality{Id=51,Name="Економіка",KnowledgeId=5,IsUsed=true},
                new Speciality{Id=52,Name="Політологія",KnowledgeId=5},
                new Speciality{Id=53,Name="Психологія",KnowledgeId=5,IsUsed=true},
                new Speciality{Id=54,Name="Соціологія",KnowledgeId=5,IsUsed=true},
                new Speciality{Id=61,Name="Журналістика",KnowledgeId=6},
                new Speciality{Id=71,Name="Облік і оподаткування",KnowledgeId=7,IsUsed=true},
                new Speciality{Id=72,Name="Фінанси, банківська справа та страхування",KnowledgeId=7},
                new Speciality{Id=73,Name="Менеджмент",KnowledgeId=7,IsUsed=true},
                new Speciality{Id=75,Name="Маркетинг",KnowledgeId=7,IsUsed=true},
                new Speciality{Id=76,Name="Підприємництво, торгівля та біржова діяльність",KnowledgeId=7},
                new Speciality{Id=81,Name="Право",KnowledgeId=8,IsUsed=true},
                new Speciality{Id=91,Name="Біологія",KnowledgeId=9,IsUsed=true},
                new Speciality{Id=101,Name="Екологія",KnowledgeId=10,IsUsed=true},
                new Speciality{Id=102,Name="Хімія",KnowledgeId=10},
                new Speciality{Id=103,Name="Науки про Землю*",KnowledgeId=10},
                new Speciality{Id=104,Name="Фізика та астрономія",KnowledgeId=10,IsUsed=true},
                new Speciality{Id=105,Name="Прикладна фізика та наноматеріали",KnowledgeId=10,IsUsed=true},
                new Speciality{Id=106,Name="Географія",KnowledgeId=10},
                new Speciality{Id=111,Name="Математика",KnowledgeId=11,IsUsed=true},
                new Speciality{Id=112,Name="Статистика",KnowledgeId=11},
                new Speciality{Id=113,Name="Прикладна математика",KnowledgeId=11,IsUsed=true},
                new Speciality{Id=121,Name="інженерія програмного забезпечення",KnowledgeId=12,IsUsed=true},
                new Speciality{Id=122,Name="Комп’ютерні науки",KnowledgeId=12,IsUsed=true},
                new Speciality{Id=123,Name="Комп’ютерна інженерія",KnowledgeId=12,IsUsed=true},
                new Speciality{Id=124,Name="Системний аналіз",KnowledgeId=12,IsUsed=true},
                new Speciality{Id=125,Name="Кібербезпека",KnowledgeId=12,IsUsed=true},
                new Speciality{Id=126,Name="інформаційні системи та технології",KnowledgeId=12},
                new Speciality{Id=131,Name="Прикладна механіка",KnowledgeId=13,IsUsed=true},
                new Speciality{Id=132,Name="Матеріалознавство",KnowledgeId=13,IsUsed=true},
                new Speciality{Id=133,Name="Галузеве машинобудування",KnowledgeId=13,IsUsed=true},
                new Speciality{Id=134,Name="Авіаційна та ракетно-космічна техніка",KnowledgeId=13,IsUsed=true},
                new Speciality{Id=135,Name="Суднобудування",KnowledgeId=13},
                new Speciality{Id=136,Name="Металургія",KnowledgeId=13,IsUsed=true},
                new Speciality{Id=141,Name="Електроенергетика, електротехніка та електромеханіка",KnowledgeId=14,IsUsed=true},
                new Speciality{Id=142,Name="Енергетичне машинобудування",KnowledgeId=14,IsUsed=true},
                new Speciality{Id=143,Name="Атомна енергетика",KnowledgeId=14,IsUsed=true},
                new Speciality{Id=144,Name="Теплоенергетика",KnowledgeId=14,IsUsed=true},
                new Speciality{Id=145,Name="Гідроенергетика",KnowledgeId=14},
                new Speciality{Id=151,Name="Автоматизація та комп’ютерно-інтегровані технології",KnowledgeId=15,IsUsed=true},
                new Speciality{Id=152,Name="Метрологія та інформаційно-вимірювальна техніка",KnowledgeId=15,IsUsed=true},
                new Speciality{Id=153,Name="Мікро- та наносистемна техніка",KnowledgeId=15,IsUsed=true},
                new Speciality{Id=161,Name="Хімічні технології та інженерія",KnowledgeId=16,IsUsed=true},
                new Speciality{Id=162,Name="Біотехнології та біоінженерія",KnowledgeId=16,IsUsed=true},
                new Speciality{Id=163,Name="Біомедична інженерія",KnowledgeId=16,IsUsed=true},
                new Speciality{Id=171,Name="Електроніка",KnowledgeId=17,IsUsed=true},
                new Speciality{Id=172,Name="Телекомунікації та радіотехніка",KnowledgeId=17,IsUsed=true},
                new Speciality{Id=173,Name="Авіоніка",KnowledgeId=17,IsUsed=true},
                new Speciality{Id=181,Name="Харчові технології",KnowledgeId=18},
                new Speciality{Id=182,Name="Технології легкої промисловості",KnowledgeId=18},
                new Speciality{Id=183,Name="Технології захисту навколишнього середовища",KnowledgeId=18},
                new Speciality{Id=184,Name="Гірництво",KnowledgeId=18,IsUsed=true},
                new Speciality{Id=185,Name="Нафтогазова інженерія та технології",KnowledgeId=18,IsUsed=true},
                new Speciality{Id=186,Name="Видавництво та поліграфія",KnowledgeId=18},
                new Speciality{Id=187,Name="Деревообробні та меблеві технології",KnowledgeId=18},
                new Speciality{Id=191,Name="Архітектура та містобудування",KnowledgeId=19},
                new Speciality{Id=192,Name="Будівництво та цивільна інженерія",KnowledgeId=19},
                new Speciality{Id=193,Name="Геодезія та землеустрій",KnowledgeId=19},
                new Speciality{Id=194,Name="Гідротехнічне будівництво, водна інженерія та водні технології",KnowledgeId=19},
                new Speciality{Id=201,Name="Агрономія",KnowledgeId=20},
                new Speciality{Id=202,Name="Захист і карантин рослин",KnowledgeId=20},
                new Speciality{Id=203,Name="Садівництво та виноградарство",KnowledgeId=20},
                new Speciality{Id=204,Name="Технологія виробництва і переробки продукції тваринництва",KnowledgeId=20},
                new Speciality{Id=205,Name="Лісове господарство",KnowledgeId=20},
                new Speciality{Id=206,Name="Садово-паркове господарство",KnowledgeId=20},
                new Speciality{Id=207,Name="Водні біоресурси та аквакультура",KnowledgeId=20},
                new Speciality{Id=208,Name="Агроінженерія",KnowledgeId=20},
                new Speciality{Id=211,Name="Ветеринарна медицина",KnowledgeId=21},
                new Speciality{Id=212,Name="Ветеринарна гігієна, санітарія і експертиза",KnowledgeId=21},
                new Speciality{Id=221,Name="Стоматологія",KnowledgeId=22},
                new Speciality{Id=222,Name="Медицина",KnowledgeId=22},
                new Speciality{Id=223,Name="Медсестринство",KnowledgeId=22},
                new Speciality{Id=224,Name="Технології медичної діагностики та лікування",KnowledgeId=22},
                new Speciality{Id=225,Name="Медична психологія",KnowledgeId=22},
                new Speciality{Id=226,Name="Фармація, промислова фармація",KnowledgeId=22},
                new Speciality{Id=227,Name="Фізична терапія, ерготерапія",KnowledgeId=22,IsUsed=true},
                new Speciality{Id=228,Name="Педіатрія",KnowledgeId=22},
                new Speciality{Id=229,Name="Громадське здоров’я",KnowledgeId=22},
                new Speciality{Id=231,Name="Соціальна робота",KnowledgeId=23},
                new Speciality{Id=232,Name="Соціальне забезпечення",KnowledgeId=23},
                new Speciality{Id=241,Name="Готельно-ресторанна справа",KnowledgeId=24},
                new Speciality{Id=242,Name="Туризм",KnowledgeId=24},
                new Speciality{Id=251,Name="Державна безпека",KnowledgeId=25},
                new Speciality{Id=252,Name="Безпека державного кордону",KnowledgeId=25},
                new Speciality{Id=253,Name="Військове управління (за видами збройних сил)",KnowledgeId=25},
                new Speciality{Id=254,Name="Забезпечення військ (сил)",KnowledgeId=25},
                new Speciality{Id=255,Name="Озброєння та військова техніка",KnowledgeId=25},
                new Speciality{Id=256,Name="Національна безпека (за окремими сферами забезпечення і видами діяльності)**",KnowledgeId=25},
                new Speciality{Id=261,Name="Пожежна безпека",KnowledgeId=26},
                new Speciality{Id=262,Name="Правоохоронна діяльність",KnowledgeId=26},
                new Speciality{Id=263,Name="Цивільна безпека",KnowledgeId=26},
                new Speciality{Id=271,Name="Річковий та морський транспорт",KnowledgeId=27},
                new Speciality{Id=272,Name="Авіаційний транспорт",KnowledgeId=27},
                new Speciality{Id=273,Name="Залізничний транспорт",KnowledgeId=27},
                new Speciality{Id=274,Name="Автомобільний транспорт",KnowledgeId=27},
                new Speciality{Id=275,Name="Транспортні технології (за видами)",KnowledgeId=27},
                new Speciality{Id=281,Name="Публічне управління та адміністрування",KnowledgeId=28,IsUsed=true},
                new Speciality{Id=291,Name="Міжнародні відносини, суспільні комунікації та регіональні студії",KnowledgeId=29},
                new Speciality{Id=292,Name="Міжнародні економічні відносини",KnowledgeId=29},
                new Speciality{Id=293,Name="Міжнародне право",KnowledgeId=29}
            };

            foreach (var e in specialities)
            {
                context.Specialities.Add(e);
            }
            context.SaveChanges();
        }

        private static void OrderInit(AspiranturaContext context)
        {
            var orders = new Order[]
            {
            new Order{Date=DateTime.Parse("2010-09-01"), Number="1",OrdertypeId=1},
            new Order{Date=DateTime.Parse("2011-09-01"), Number="79",OrdertypeId=4},
            new Order{Date=DateTime.Parse("2012-09-01"), Number="53",OrdertypeId=1},
            new Order{Date=DateTime.Parse("2014-03-01"), Number="3",OrdertypeId=2},
            new Order{Date=DateTime.Parse("2015-05-30"), Number="3",OrdertypeId=3}
            };
            foreach (Order item in orders)
            {
                context.Orders.Add(item);
            }
            context.SaveChanges();
        }

        private static void AspirantInit(AspiranturaContext context)
        {
            var aspirants = new Aspirant[]
            {
            new Aspirant{Name="Carson",Surename="Alexander",Birthday=DateTime.Parse("2005-09-01"), Patronymic="Петрович", StatustypeId=(int)StatusTypeEnum.новий, SpecialityId=21, InputDate=DateTime.Parse("2010-09-01")},
            new Aspirant{Name="Meredith",Surename="Alonso",Birthday=DateTime.Parse("2002-09-01"), Patronymic="Васильевич", StatustypeId=(int)StatusTypeEnum.новий, SpecialityId=21},
            new Aspirant{Name="Arturo",Surename="Anand",Birthday=DateTime.Parse("2003-09-01"), StatustypeId=(int)StatusTypeEnum.навчання, SpecialityId=21},
            new Aspirant{Name="Gytis",Surename="Barzdukas",Birthday=DateTime.Parse("2002-09-01"), StatustypeId=(int)StatusTypeEnum.новий, SpecialityId=21},
            new Aspirant{Name="Yan",Surename="Li",Birthday=DateTime.Parse("2002-09-01"), StatustypeId=(int)StatusTypeEnum.відрахований_2, SpecialityId=21},
            new Aspirant{Name="Peggy",Surename="Justice",Birthday=DateTime.Parse("2001-09-01"), StatustypeId=(int)StatusTypeEnum.відрахований_2, SpecialityId=21},
            new Aspirant{Name="Laura",Surename="Norman",Birthday=DateTime.Parse("2003-09-01"), StatustypeId=(int)StatusTypeEnum.захист_достроково, SpecialityId=21},
            new Aspirant{Name="Nino",Surename="Olivetto",Birthday=DateTime.Parse("2005-09-01"), StatustypeId=(int)StatusTypeEnum.стажування, SpecialityId=21}
            };
            foreach (Aspirant asp in aspirants)
            {
                context.Aspirants.Add(asp);
            }
            context.SaveChanges();


           // var aspirantOrders = new AspirantOrder[]
           // {
           //      new AspirantOrder {
           //          AspirantId = aspirants.Single(a => a.Name == "Carson").Id,
           //          OrderId = 1
           //          },
           //      new AspirantOrder {
           //          AspirantId = aspirants.Single(a => a.Name == "Meredith").Id,
           //          OrderId = 1
           //          }           
           //};
           // foreach (AspirantOrder ao in aspirantOrders)
           // {
           //     context.AspirantOrders.Add(ao);
           // }
           // context.SaveChanges();


        }
    }
}