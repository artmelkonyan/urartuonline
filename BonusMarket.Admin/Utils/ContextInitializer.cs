using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Admin.Models;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Models.Core.User;
using BonusMarket.Shared.Services;

namespace BonusMarket.Admin.Utils
{
    public class ContextInitializer
    {
        public static void Initialize(Context context, EncryptionService encryptionService)
        {
            // context.Database.EnsureCreated();
//            context.Database.Migrate();

            // init permissions
            if (!context.Permissions.Any())
            {
                context.Permissions.Add(new Permission()
                {
                    ModuleNumber = (int)Modules.Auth,
                    ModuleName = "AuthModule.Login",
                    PermissionName = "AuthLogin",
                    Description = "Permissions are only for Developers",
                    PermissionNumber = (int)AuthModule.Login,
                });
                context.Permissions.Add(new Permission()
                {
                    ModuleNumber = (int)Modules.Admin,
                    ModuleName = "AdminModule.Index",
                    PermissionName = "AdminIndex",
                    Description = "Permissions are only for Developers",
                    PermissionNumber = (int)AdminModule.Index,
                });

                context.SaveChanges();
            }
            
            // init roles
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role()
                {
                    Name = "Default",
                    SystemName = "Default",
                    Active = true,
                    CreationDate = DateTime.Now,
                    Description = "Default Role",
                    SystemRole = true
                });
                context.Roles.Add(new Role()
                {
                    Name = "Admin",
                    SystemName = "Admin",
                    Active = true,
                    CreationDate = DateTime.Now,
                    Description = "Admin Role",
                    SystemRole = true
                });


                context.SaveChanges();
            }
            
            // init role permissions
            if (!context.RolePermissions.Any())
            {
                var currentPermission = context.Permissions.Where(r => r.PermissionName == "AuthLogin").ToList()[0];
                var currentRole = context.Roles.Where(r => r.SystemName == "Default").ToList()[0];

                context.RolePermissions.Add(new RolePermission()
                {
                    Role = currentRole,
                    Permission = currentPermission,
                });
                
                currentPermission = context.Permissions.Where(r => r.PermissionName == "AdminIndex").ToList()[0];
                currentRole = context.Roles.Where(r => r.SystemName == "Default").ToList()[0];

                context.RolePermissions.Add(new RolePermission()
                {
                    Role = currentRole,
                    Permission = currentPermission,
                });

                context.SaveChanges();
            }

            if (!context.Users.Where(r => r.Email == "admin@urartuonline.am").Any())
            {
                string password = "urartu321";
                // string salt = encryptionService.HashedSaltGenerator();
                // string hashedPassword = encryptionService.CryptPasswordWithSalt(password: password, HashedSalt: salt);
                string hashedPassword = encryptionService.CalculateMD5Hash(password);
                
                context.Users.Add(new User()
                {
                    Email = "admin@urartuonline.am",
                    CreationDate = DateTime.Now,
                    FirstName = "System",
                    LastName = "System Adminyan Useri",
                    Phone = "+37",
                    Address = "Vaspurakan 8",
                    PasswordHash = hashedPassword,
                    Role = UserRoleEnum.ROLE_GUEST
                });

                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {
                var currentUser = context.Users.Where(r => r.Email == "admin@urartuonline.am").ToList()[0];
                var currentRole = context.Roles.Where(r => r.SystemName == "Default").ToList()[0];
                context.UserRoles.Add(new UserRole()
                {
                    User = currentUser,
                    Role = currentRole
                });
                currentRole = context.Roles.Where(r => r.SystemName == "Admin").ToList()[0];
                context.UserRoles.Add(new UserRole()
                {
                    User = currentUser,
                    Role = currentRole
                });
                context.SaveChanges();
            }
            
            return;
//            
//            if (context.Books.Any() || context.Authors.Any() || context.Categories.Any())
//                return;
//
//
//            // Adding Top level categories
//            context.Categories.Add(
//                new Category
//                {
//                    Level = 0,
//                    BannerImage = "",
//                    Key = "root"
//                }
//            );
//
//            context.SaveChanges();
//
//            // Adding level 1 categories
//            context.Categories.AddRange(
//                new Category[]
//                {
//                    new Category
//                    {
//                        Level = 1,
//                        BannerImage = "",
//                        Key = "classic",
//                        ParentCategory = context.Categories.Where(c => c.Key == "root").First()
//                    },
//                    new Category
//                    {
//                        Level = 1,
//                        BannerImage = "",
//                        Key = "fantasy",
//                        ParentCategory = context.Categories.Where(c => c.Key == "root").First()
//                    },
//                    new Category
//                    {
//                        Level = 1,
//                        BannerImage = "",
//                        Key = "fiction",
//                        ParentCategory = context.Categories.Where(c => c.Key == "root").First()
//                    }
//                }
//            );
//
//            context.SaveChanges();
//
//            // Adding level 2 categories
//            context.Categories.AddRange(
//                new Category[]
//                {
//                    new Category
//                    {
//                        Level = 2,
//                        BannerImage = "",
//                        Key = "romance",
//                        ParentCategory = context.Categories.Where(c => c.Key == "fiction").First()
//                    },
//                    new Category
//                    {
//                        Level = 2,
//                        BannerImage = "",
//                        Key = "drama",
//                        ParentCategory = context.Categories.Where(c => c.Key == "classic").First()
//                    },
//                    new Category
//                    {
//                        Level = 1,
//                        BannerImage = "",
//                        Key = "fantastic",
//                        ParentCategory = context.Categories.Where(c => c.Key == "fantasy").First()
//                    }
//                }
//            );
//            context.SaveChanges();
//
//            // Adding Category translations
//            context.CategoryTranslations.AddRange(
//                new CategoryTranslation[] {
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Fantasy",
//                        Category = context.Categories.Where(c => c.Key == "fantasy").First()
//                    },
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Classic",
//                        Category = context.Categories.Where(c => c.Key == "classic").First()
//                    },
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Fiction",
//                        Category = context.Categories.Where(c => c.Key == "fiction").First()
//                    },
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Romance",
//                        Category = context.Categories.Where(c => c.Key == "romance").First()
//                    },
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Drama",
//                        Category = context.Categories.Where(c => c.Key == "drama").First()
//                    },
//                    new CategoryTranslation
//                    {
//                        Language = "en",
//                        Title = "Fantastic",
//                        Category = context.Categories.Where(c => c.Key == "fantastic").First()
//                    }
//                }
//            );
//
//            context.SaveChanges();
//
//            // Adding Books
//            context.Books.AddRange(
//                new Book[]
//                {
//                    new Book
//                    {
//                        Isbn = "9789939688107",
//                        Year = 2020,
//                        IsActive = true,
//                        IsVat = true,
//                        Price = 3400,
//                        PublicationType = "SoftCover",
//                        Size = "130x200",
//                        Weight = 350,
//                        PagesCount = 288,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939688565",
//                        Year = 2020,
//                        IsActive = true,
//                        IsVat = true,
//                        Price = 6400,
//                        PublicationType = "HardCover",
//                        Size = "170x243",
//                        Weight = 680,
//                        PagesCount = 368,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939688084",
//                        Year = 2020,
//                        IsActive = true,
//                        IsVat = false,
//                        Price = 3700,
//                        PublicationType = "HardCover",
//                        Size = "152x222",
//                        Weight = 350,
//                        PagesCount = 160,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939682044",
//                        Year = 2013,
//                        IsActive = true,
//                        IsVat = false,
//                        Price = 3200,
//                        PublicationType = "SoftCover",
//                        Size = "145x210",
//                        Weight = 305,
//                        PagesCount = 224,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939688121",
//                        Year = 2020,
//                        IsActive = true,
//                        IsVat = false,
//                        Price = 3600,
//                        PublicationType = "HardCover",
//                        Size = "129x195",
//                        Weight = 400,
//                        PagesCount = 224,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939684536",
//                        Year = 2016,
//                        ReYear = 2020,
//                        IsActive = true,
//                        IsVat = true,
//                        Price = 2800,
//                        PublicationType = "SoftCover",
//                        Size = "120x200",
//                        Weight = 260,
//                        PagesCount = 272,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939685380",
//                        Year = 2017,
//                        IsActive = true,
//                        IsVat = true,
//                        Price = 7500,
//                        PublicationType = "HardCover",
//                        Size = "220x305",
//                        Weight = 825,
//                        PagesCount = 104,
//                        Language = "Russian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939683386",
//                        Year = 2014,
//                        IsActive = true,
//                        IsVat = false,
//                        Price = 3400,
//                        PublicationType = "HardCover",
//                        Size = "245x345",
//                        Weight = 620,
//                        PagesCount = 44,
//                        Language = "Eastern Armenian"
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939685427",
//                        IsActive = true,
//                        IsVat = true,
//                        Price = 10900,
//                        PublicationType = "HardCover",
//                        Size = "200x285",
//                        Weight = 900,
//                        PagesCount = 164
//                    },
//                    new Book
//                    {
//                        Isbn = "9789939688145",
//                        IsActive = true,
//                        IsVat = false,
//                        Price = 3700,
//                        Size = "140x215",
//                        Weight = 600,
//                        PagesCount = 432
//                    }
//                }
//            );
//            context.SaveChanges();
//
//            // Adding Book Translations
//            context.BookTranslations.AddRange(
//                new BookTranslation[]
//                {
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Title = "Դուբլինցիներ",
//                        ShortDescription = "Ջեյմս Ջոյսը իր «Դուբլինցիները» (1914) պատմվածքների ժողովածուում միտում է ունեցել պատկերելու հայրենի Իռլանդիայի հոգեկան կյանքի պատմությունը՝ գործողու­թյունների կենտրոն դարձնելով Դուբլինը՝ որպես «կաթ­վածի կենտրոն»: Հեղինակը ներկայացնում է քաղաքը, միջին խավի մարդ կանց, անտարբերության մթնոլորտը, եւ դա անում է չորս հայեցակետով՝ մանկություն, պատանե­կություն, հասունություն եւ հասարակական կյանք: Չնա­յած ճիգերին՝ նրա հերոսները չեն կարողանում դուրս գալ փակուղուց, փոխել իրենց կյանքի միօրինակ գունապնակը, բայցեւ երազող են, երազ, որ փախուստի աշխարհն է: «Դուբլինցիներ» –ում առկա կարեւորագույն երեւույթը էպի­ֆանիան է՝ աստվածահայտնությունը: Հարկ է նշել, որ «Դուբլինցիները» հայերեն թարգմանու­թյամբ առաջին անգամ է լույս տեսնում համալրված` ներա­ռելով նախկինում չթարգմանված պատմվածքները:",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Title = "Dubliners",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688565").First(),
//                        Title = "Հազար չքնաղ արևներ",
//                        ShortDescription = "2007 թվականին լույս տեսած այս վեպը անկեղծ ու հուզիչ գիրք է Աֆղանստանի ոչ վաղ անցյալի մասին` այս անգամ կանանց աչքերով: Վեպի հիմնական թեման Աֆղանստանում կանանց իրավունքների ոտնահարումն է, նրանց հանդեպ բռնությունն ու դաժանությունը: Զրուցելով Քաբուլի բազմաթիվ կանանց հետ և լսելով նրանց հոգեկեղեք պատմությունները՝ Հոսեյնին գրել է «Հազար չքնաղ արևները» և նվիրել այն Աֆղանստանի կանանց: Վեպը օգնում է ավելի լավ հասկանալու Աֆղանստանի դժնդակ պատմությունը, մշակույթն ու ժառանգությունը, իսկ անմարդկային պայմաններում ապրող ժողովրդի հերոսությունը կարող է միայն հիացմունք առաջացնել:",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688565").First(),
//                        Title = "A Thousand Splendid Suns",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688084").First(),
//                        Title = "Մումիտրոլներն ու դիսաստղը",
//                        ShortDescription = "Օրեցօր մեծացող գիսաստղը խախտում է մումիտրոլների անդորրը: Մումիտրոլն ու Սնիֆը որոշում են պարզել, թե ինչ է գիսաստղը, և երբ է այն բախվելու Երկիր մոլորակին: Արկածներով լի դեգերումներից հետո նրանց հաջողվում է պարզել գիսաստղի ընկնելու օրն ու ժամը… և փրկվել կործանումից:",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688084").First(),
//                        Title = "Comet in Moominland",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939682044").First(),
//                        Title = "Չիպոլինոյի արկածները",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939682044").First(),
//                        Title = "Adventures of Cipollino",
//                        ShortDescription = "What a fruitful and animated world of human like vegetables is created in Rodari’s work! Just take a look at the names of the inhabitants of that garden kingdom: Cipollino – the little onion, Signor Tomato, Prince Lemon which speaks of the traits and habits of their bearers. The real and fictive worlds interweave and poignant and acute humor fills the pages which doesn’t prevent the author from addressing the gravest problems including tolerance and peace between different strata of society.",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688121").First(),
//                        Title = "Մտածել ինչպես Սթիվ Ջոբսը",
//                        ShortDescription = "Այս ոգեշնչող գրքի էջերում Դանիել Սմիթն անդրադառնում է Ջոբսի կյանքին և մասնագիտական գործունեությանը, ինչպես նաև մեջբերում այս մեծ մարդու կողմից և նրա մասին ասված հայտնի խոսքերը՝ ներկայացնելու այն փիլիսոփայությունը, բիզնես դասերը, անգամ տպավորիչ ձախողումները, որոնք օգնել են նրան հասնելու անհավանական ձեռքբերումների։ Գիրքը ուղղորդում է ձեզ նայելու աշխարհին հզոր երևակայության տեր հանճարի աչքերով։",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688121").First(),
//                        Title = "How To Think Like Steve Jobs",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939684536").First(),
//                        Title = "Երփներանգ շղարշը",
//                        ShortDescription = "Վեպը քննարկում է Անգլիայի պատմության այն բեկում­ նային շրջանը, երբ կանայք թեպետ ունեին ընտրելու իրա­ վունք, սակայն, ճնշվելով նախապաշարումներից եւ նախկին ավանդույթների անբեկանելիությունից, ըստ էության զրկված էին այդ ընտրությունն իրականացնելու հնարավորությունից: Պատմության հերոսուհին՝ Քիթին, տեղի տալով մոր հորդոր­ ներին, ամուսնանում է երիտասարդ բժշկի հետ, ում հանդեպ անտարբեր է, եւ շուտով սիրավեպ է սկսում մեկ այլ տղամար­ դու հետ. հետեւանքները լինում են ավելի քան անսպասելի ու անդառնալի: Ի վերջո Քիթին հասկանում է, որ աղջիկներին պետք է դաստիարակեն ոչ թե որպես հեզ ու անխելք տանտի­ կիններ, այլ լայնախոհ ու անկախ անհատականություններ:",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939684536").First(),
//                        Title = "The Painted Veil",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939685380").First(),
//                        Title = "10 հայ ականավոր թագուհիներ",
//                        ShortDescription = "Գրքում ներկայացված տասը հայ ականավոր թագուհիներն ընտրված են տարբեր դարաշրջաններից: Ընտրվել են այնպիսի գործիչներ, ովքեր իրենց բնորոշ գծերով առանձնացել են ոչ միայն ժամանակակիցներից, այլև զգալի հետք են թողել մեր ողջ պատմության մեջ:      Նյութը շարադրված է գիտահանրամատչելի լեզվով: Որպեսզի ընթերցողը հեշտությամբ կարողանա մանրամասներ գտնել ներկայացված ականավոր թագուհիների մասին, յուրաքանչյուր ակնարկից հետո նշված են հիմնական աղբյուրներն ու ուսումնասիրությունները:",
//                        Language = "Ռուսերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939685380").First(),
//                        Title = "10 Outstanding Armenian Queens",
//                        Language = "Russian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939683386").First(),
//                        Title = "Օլիվեր Թվիստ",
//                        ShortDescription = "Օլիվեր Թվիստը մեծանում է մանկատանը` օտար ու անտարբեր մարդկանց միջավայրում: Ի վերջո, նա որոշում է փախչել Լոնդոն: Մեծ քաղաքը, սակայն, նույնքան անտարբեր է տղայի հանդեպ: Ֆեյգին անունով խաբեբան փորձում է նրան այլ փոքրիկների նման գրպանահատ դարձնել: Բարեբախտաբար, Օլիվերը հանդիպում է բարի մարդկանց, և դեպքերն այլ ընթացք են ստանում…",
//                        Language = "Արևելահայերեն",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939683386").First(),
//                        Title = "Oliver Twist",
//                        ShortDescription = "Oliver Twist is an orphan. He is nine years old and works as an apprentice for a funeral director. But life is hard and Oliver decides to run away to London. When he reaches the capital, he meets a young pickpocket who works for a villain called Fagin. Oliver falls into Fagin's clutches and is taught to steal from the rich...",
//                        Language = "Eastern Armenian",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939685427").First(),
//                        Title = "Երևան. ճեպանկարների գիրք",
//                        ShortDescription = "«Երևան. ճեպանկարների գիրքը» ներկայացնում է մայրաքաղաքը ոչ միայն իր հարուստ պատմությամբ ու տեսարժան վայրերով, այլև գույներով ու բույրերով, աղմուկով ու երաժշտությամբ, հայտնի ու անհայտ բնակիչներով, ամեն վայրկյան տրոփող առօրյա կյանքով: Նկարիչներ Արարատ Մինասյանն ու Զաքար Դեմիրճյանը երբեմն հպանցիկ տպավորություններով, երբեմն էլ հիմնավոր ու խոհուն հայացքով երևանցիներին ու մայրաքաղաքի հյուրերին են փոխանցում հարուստ անցյալի ու աշխույժ ներկայի, Արևելքի ու Արևմուտքի խաչմերուկում կանգնած քաղաքի իրենց պատկերը՝ գունագեղ ու խայտաբղետ, տեղ–տեղ սև ու սպիտակ, սակայն երևանցիների համար մշտապես սիրելի, իսկ հյուրերի համար հետաքրքիր ու անսպասելի բացահայտումներով լի: Թեթև ուրվագծերով, երբեմն էլ թանձր վրձնահարվածներով ճեպանկարների միջոցով ներկայացող հին ու նոր Երևանի պատկերները է՛լ ավելի հարազատ կդարձնեն մայրաքաղաքը հայերի համար, իսկ զբոսաշրջիկների երևանյան ուղևորությունն այս գրքի ուղեկցությամբ անմոռանալի կդառնա:",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939685427").First(),
//                        Title = "Yerevan Sketchbook",
//                        ShortDescription = "“Yerevan Sketchbook” introduces the capital city with not only its rich history and sites worth seeing, but also with its hues and scents, its noise and music, its famous and not so famous residents and simply with its daily and vibrant life. Artists Ararat Minasyan and Zack Demirtshyan, alternating between whimsical momentary impressions and profound ideological expressions, depict scenes of the rich past and the lively present of this locale, situated in the crossroads of the East and the West. The city is presented either in vibrant colors or in black and white, but is always lovely for the residents, and full of interesting and unexpected discoveries for its guests. With light outlines and sometimes with heavy brushstrokes, these sketches will make the images of the old and new Yerevan more familiar to Armenians. As for visitors, this book will make their trip to Yerevan a trip to remember.",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688145").First(),
//                        Title = "Առանց ընտանիքի: Արկածներ որոնողները մատենաշար",
//                        ShortDescription = "«Առանց ընտանիքի» վեպը ֆրանսիացի վիպասան Հեկտոր Մալոյի ամենահայտնի երկն է: Վեպի գլխավոր հերոսը Ռեմին է. ընկեցիկ մի տղա, որին տասը տարեկան հասակում հայրացուն՝ Բարբերենը, վաճառում է շրջիկ երաժիշտ Վիտալիսին: Սկսվում են Ռեմիի հետաքրքիր արկածները Ֆրանսիայի ու Անգլիայի գյուղերում ու քաղաքներում: Չնայած բազմաթիվ դժվարություններին՝ Ռեմին երբեք չի հուսահատվում և իր բարեկամների՝ Մատտիա երաժշտի, Կապի շան և Հոգյակ անունով կապիկի հետ հաղթահարում է բոլոր փորձությունները:",
//                        IsActive = true
//                    },
//                    new BookTranslation
//                    {
//                        Book = context.Books.Where(b => b.Isbn == "9789939688145").First(),
//                        Title = "Nobody's boy",
//                        IsActive = true
//                    }
//                }
//            );
//            context.SaveChanges();
//
//
//            // Adding authors
//            context.Authors.AddRange(
//                new Author[]
//                {
//                    new Author
//                    {
//                        Title = "Ջեյմս Ջոյս",
//                        Key = "jamesjoyce"
//                    },
//                    new Author
//                    {
//                        Title = "Արամ Արսենյան",
//                        Key = "aramarsenyan"
//                    },
//                    new Author
//                    {
//                        Title = "Արտեմ Հարությունյան",
//                        Key = "artemharutyunyan"
//                    },
//                    new Author
//                    {
//                        Title = "Գայանե Հարությունյան",
//                        Key = "gayaneharutyunyan"
//                    }
//                }
//            );
//            context.SaveChanges();
//
//
//            // Adding Book Authors
//            context.BookAuthors.AddRange(
//                new BookAuthor[]
//                {
//                    new BookAuthor
//                    {
//                        Author = context.Authors.Where(a => a.Key == "jamesjoyce").First(),
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Role = "Author"
//                    },
//                    new BookAuthor
//                    {
//                        Author = context.Authors.Where(a => a.Key == "aramarsenyan").First(),
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Role = "Translator"
//                    },
//                    new BookAuthor
//                    {
//                        Author = context.Authors.Where(a => a.Key == "artemharutyunyan").First(),
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Role = "Translator"
//                    },
//                    new BookAuthor
//                    {
//                        Author = context.Authors.Where(a => a.Key == "gayaneharutyunyan").First(),
//                        Book = context.Books.Where(b => b.Isbn == "9789939688107").First(),
//                        Role = "Translator"
//                    }
//                }
//            );


            // ❗❗❗❗❗ To continue from here
        }
    }
}