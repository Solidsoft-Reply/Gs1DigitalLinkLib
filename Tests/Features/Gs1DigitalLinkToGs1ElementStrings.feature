Feature: Gs1DigitalLinkToGs1ElementStrings

Scenario Outline: Convert a Digital Link to an element string
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                                          | elementStrings                                                         |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123                      | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	  | https://id.gs1.org/01/5412345000013/22/AJY3095721P49/10/ABC123                       | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/21/72292641703           | 2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201             |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703                        | 01054123450000132172292641703<GS>10ABC123                              |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33                                    | 80040541234537290A41&GHpp33                                            |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123                                 | 10ABC123<GS>8006054123450000130201                                     |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33/7040/5BVR                          | 70405BVR<GS>80040541234537290A41&GHpp33                                |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123/21/72292641703 | 2172292641703<GS>10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201 |
	  | https://id.gs1.org/412/5060917510004                                                 | 4125060917510004                                                       |
	  | https://id.gs1.org/401/0541234537290A41%26GHpp33                                     | 4010541234537290A41&GHpp33                                             |
	  | https://id.gs1.org/417/5060917510004                                                 | 4175060917510004                                                       |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49                          | 22AJY3095721P49<GS>8006054123450000130201                              |
	  | https://id.gs1.org/414/5060917510004/7040/5KFX                                       | 414506091751000470405KFX                                               |
	  | https://id.gs1.org/410/5060917510004                                                 | 4105060917510004                                                       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/21/72292641703                 | 01054123450000132172292641703<GS>22AJY3095721P49                       |
	  | https://id.gs1.org/416/5060917510004                                                 | 4165060917510004                                                       |
	  | https://id.gs1.org/8017/817019610850151481/8019/4003812                              | 80194003812<GS>8017817019610850151481                                  |
	  | https://id.gs1.org/8006/054123450000130201                                           | 8006054123450000130201                                                 |
	  | https://id.gs1.org/411/5060917510004                                                 | 4115060917510004                                                       |
	  | https://id.gs1.org/8017/817019610850151481                                           | 8017817019610850151481                                                 |
	  | https://id.gs1.org/414/5060917510004/254/RP56J920471                                 | 4145060917510004254RP56J920471                                         |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33                                      | 80100541234537290A41#GH33                                              |
	  | https://id.gs1.org/415/5060917510004                                                 | 4155060917510004                                                       |
	  | https://id.gs1.org/01/05412345000013/10/ABC123                                       | 010541234500001310ABC123                                               |
	  | https://id.gs1.org/8018/411369833821910265                                           | 8018411369833821910265                                                 |
	  | https://id.gs1.org/402/21173492661712079                                             | 40221173492661712079                                                   |
	  | https://id.gs1.org/01/05412345000013                                                 | 0105412345000013                                                       |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123/21/72292641703                  | 2172292641703<GS>10ABC123<GS>8006054123450000130201                    |
	  | https://id.gs1.org/8006/054123450000130201/21/72292641703                            | 2172292641703<GS>8006054123450000130201                                |
	  | https://id.gs1.org/8013/5060917511473062B98T                                         | 80135060917511473062B98T                                               |
	  | https://id.gs1.org/413/5060917510004                                                 | 4135060917510004                                                       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49                                | 010541234500001322AJY3095721P49                                        |
	  | https://id.gs1.org/8018/411369833821910265/8019/4003812                              | 80194003812<GS>8018411369833821910265                                  |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33/8011/6639047221                      | 80116639047221<GS>80100541234537290A41#GH33                            |
	  | https://id.gs1.org/417/5060917510004/7040/5KFX                                       | 417506091751000470405KFX                                               |
	  | https://id.gs1.org/00/998440410914660971                                             | 00998440410914660971                                                   |
	  | https://id.gs1.org/414/5060917510004                                                 | 4145060917510004                                                       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123/21/72292641703       | 01054123450000132172292641703<GS>10ABC123<GS>22AJY3095721P49           |
	  | https://id.gs1.org/8003/04965031954585                                               | 800304965031954585                                                     |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123                | 10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201                  |
	  | https://id.gs1.org/01/05412345000013/21/72292641703                                  | 01054123450000132172292641703                                          |

Scenario Outline: Convert a Digital Link to an element string with brackets
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string with brackets
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                                          | elementStrings                                                     |
	  | https://id.gs1.org/8018/411369833821910265/8019/4003812                              | (8018)411369833821910265(8019)4003812                              |
	  | https://id.gs1.org/412/5060917510004                                                 | (412)5060917510004                                                 |
	  | https://id.gs1.org/416/5060917510004                                                 | (416)5060917510004                                                 |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123                      | (01)05412345000013(22)AJY3095721P49(10)ABC123                      |
	  | https://id.gs1.org/01/5412345000013/22/AJY3095721P49/10/ABC123                       | (01)05412345000013(22)AJY3095721P49(10)ABC123                      |
	  | https://id.gs1.org/8013/5060917511473062B98T                                         | (8013)5060917511473062B98T                                         |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123                                 | (8006)054123450000130201(10)ABC123                                 |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123/21/72292641703                  | (8006)054123450000130201(10)ABC123(21)72292641703                  |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33                                      | (8010)0541234537290A41#GH33                                        |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33/7040/5BVR                          | (8004)0541234537290A41&GHpp33(7040)5BVR                            |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49                          | (8006)054123450000130201(22)AJY3095721P49                          |
	  | https://id.gs1.org/8003/04965031954585                                               | (8003)04965031954585                                               |
	  | https://id.gs1.org/01/05412345000013/10/ABC123                                       | (01)05412345000013(10)ABC123                                       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123/21/72292641703       | (01)05412345000013(22)AJY3095721P49(10)ABC123(21)72292641703       |
	  | https://id.gs1.org/8018/411369833821910265                                           | (8018)411369833821910265                                           |
	  | https://id.gs1.org/415/5060917510004                                                 | (415)5060917510004                                                 |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/21/72292641703           | (8006)054123450000130201(22)AJY3095721P49(21)72292641703           |
	  | https://id.gs1.org/8017/817019610850151481/8019/4003812                              | (8017)817019610850151481(8019)4003812                              |
	  | https://id.gs1.org/01/05412345000013                                                 | (01)05412345000013                                                 |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123                | (8006)054123450000130201(22)AJY3095721P49(10)ABC123                |
	  | https://id.gs1.org/01/05412345000013/21/72292641703                                  | (01)05412345000013(21)72292641703                                  |
	  | https://id.gs1.org/00/998440410914660971                                             | (00)998440410914660971                                             |
	  | https://id.gs1.org/417/5060917510004/7040/5KFX                                       | (417)5060917510004(7040)5KFX                                       |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33                                    | (8004)0541234537290A41&GHpp33                                      |
	  | https://id.gs1.org/8006/054123450000130201                                           | (8006)054123450000130201                                           |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703                        | (01)05412345000013(10)ABC123(21)72292641703                        |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49                                | (01)05412345000013(22)AJY3095721P49                                |
	  | https://id.gs1.org/402/21173492661712079                                             | (402)21173492661712079                                             |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33/8011/6639047221                      | (8010)0541234537290A41#GH33(8011)6639047221                        |
	  | https://id.gs1.org/8006/054123450000130201/21/72292641703                            | (8006)054123450000130201(21)72292641703                            |
	  | https://id.gs1.org/410/5060917510004                                                 | (410)5060917510004                                                 |
	  | https://id.gs1.org/411/5060917510004                                                 | (411)5060917510004                                                 |
	  | https://id.gs1.org/401/0541234537290A41%26GHpp33                                     | (401)0541234537290A41&GHpp33                                       |
	  | https://id.gs1.org/417/5060917510004                                                 | (417)5060917510004                                                 |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/21/72292641703                 | (01)05412345000013(22)AJY3095721P49(21)72292641703                 |
	  | https://id.gs1.org/8017/817019610850151481                                           | (8017)817019610850151481                                           |
	  | https://id.gs1.org/414/5060917510004/254/RP56J920471                                 | (414)5060917510004(254)RP56J920471                                 |
	  | https://id.gs1.org/414/5060917510004/7040/5KFX                                       | (414)5060917510004(7040)5KFX                                       |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123/21/72292641703 | (8006)054123450000130201(22)AJY3095721P49(10)ABC123(21)72292641703 |
	  | https://id.gs1.org/414/5060917510004                                                 | (414)5060917510004                                                 |
	  | https://id.gs1.org/413/5060917510004                                                 | (413)5060917510004                                                 |

Scenario Outline: Convert a Digital Link using short text to an element string
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                                             | elementStrings                                                         |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123                     | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	  | https://id.gs1.org/gtin/5412345000013/cpv/AJY3095721P49/lot/ABC123                      | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/ser/72292641703            | 2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201             |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123/ser/72292641703                       | 01054123450000132172292641703<GS>10ABC123                              |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33                                       | 80040541234537290A41&GHpp33                                            |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123                                   | 10ABC123<GS>8006054123450000130201                                     |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33/7040/5BVR                             | 70405BVR<GS>80040541234537290A41&GHpp33                                |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123/ser/72292641703 | 2172292641703<GS>10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201 |
	  | https://id.gs1.org/purchasedFrom/5060917510004                                          | 4125060917510004                                                       |
	  | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                                       | 4010541234537290A41&GHpp33                                             |
	  | https://id.gs1.org/party/5060917510004                                                  | 4175060917510004                                                       |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49                            | 22AJY3095721P49<GS>8006054123450000130201                              |
	  | https://id.gs1.org/gln/5060917510004/7040/5KFX                                          | 414506091751000470405KFX                                               |
	  | https://id.gs1.org/shipTo/5060917510004                                                 | 4105060917510004                                                       |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/ser/72292641703                | 01054123450000132172292641703<GS>22AJY3095721P49                       |
	  | https://id.gs1.org/glnProd/5060917510004                                                | 4165060917510004                                                       |
	  | https://id.gs1.org/gsrnp/817019610850151481/srin/4003812                                | 80194003812<GS>8017817019610850151481                                  |
	  | https://id.gs1.org/itip/054123450000130201                                              | 8006054123450000130201                                                 |
	  | https://id.gs1.org/billTo/5060917510004                                                 | 4115060917510004                                                       |
	  | https://id.gs1.org/gsrnp/817019610850151481                                             | 8017817019610850151481                                                 |
	  | https://id.gs1.org/gln/5060917510004/glnx/RP56J920471                                   | 4145060917510004254RP56J920471                                         |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33                                         | 80100541234537290A41#GH33                                              |
	  | https://id.gs1.org/payTo/5060917510004                                                  | 4155060917510004                                                       |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123                                       | 010541234500001310ABC123                                               |
	  | https://id.gs1.org/gsrn/411369833821910265                                              | 8018411369833821910265                                                 |
	  | https://id.gs1.org/gsin/21173492661712079                                               | 40221173492661712079                                                   |
	  | https://id.gs1.org/gtin/05412345000013                                                  | 0105412345000013                                                       |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123/ser/72292641703                   | 2172292641703<GS>10ABC123<GS>8006054123450000130201                    |
	  | https://id.gs1.org/itip/054123450000130201/ser/72292641703                              | 2172292641703<GS>8006054123450000130201                                |
	  | https://id.gs1.org/gmn/5060917511473062B98T                                             | 80135060917511473062B98T                                               |
	  | https://id.gs1.org/shipFor/5060917510004                                                | 4135060917510004                                                       |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49                                | 010541234500001322AJY3095721P49                                        |
	  | https://id.gs1.org/gsrn/411369833821910265/srin/4003812                                 | 80194003812<GS>8018411369833821910265                                  |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33/cpsn/6639047221                         | 80116639047221<GS>80100541234537290A41#GH33                            |
	  | https://id.gs1.org/party/5060917510004/7040/5KFX                                        | 417506091751000470405KFX                                               |
	  | https://id.gs1.org/sscc/998440410914660971                                              | 00998440410914660971                                                   |
	  | https://id.gs1.org/gln/5060917510004                                                    | 4145060917510004                                                       |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123/ser/72292641703     | 01054123450000132172292641703<GS>10ABC123<GS>22AJY3095721P49           |
	  | https://id.gs1.org/grai/04965031954585                                                  | 800304965031954585                                                     |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123                 | 10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201                  |
	  | https://id.gs1.org/gtin/05412345000013/ser/72292641703                                  | 01054123450000132172292641703                                          |

Scenario Outline: Convert a Digital Link using short text to an element string with brackets
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string with brackets
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                                             | elementStrings                                                     |
	  | https://id.gs1.org/gtin/05412345000013/ser/72292641703                                  | (01)05412345000013(21)72292641703                                  |
	  | https://id.gs1.org/gtin/5412345000013/ser/72292641703                                   | (01)05412345000013(21)72292641703                                  |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33                                         | (8010)0541234537290A41#GH33                                        |
	  | https://id.gs1.org/gln/5060917510004                                                    | (414)5060917510004                                                 |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123                     | (01)05412345000013(22)AJY3095721P49(10)ABC123                      |
	  | https://id.gs1.org/gsrn/411369833821910265                                              | (8018)411369833821910265                                           |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123/ser/72292641703     | (01)05412345000013(22)AJY3095721P49(10)ABC123(21)72292641703       |
	  | https://id.gs1.org/grai/04965031954585                                                  | (8003)04965031954585                                               |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33                                       | (8004)0541234537290A41&GHpp33                                      |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49                                | (01)05412345000013(22)AJY3095721P49                                |
	  | https://id.gs1.org/party/5060917510004                                                  | (417)5060917510004                                                 |
	  | https://id.gs1.org/gsrnp/817019610850151481                                             | (8017)817019610850151481                                           |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123                                   | (8006)054123450000130201(10)ABC123                                 |
	  | https://id.gs1.org/itip/054123450000130201                                              | (8006)054123450000130201                                           |
	  | https://id.gs1.org/gln/5060917510004/7040/5KFX                                          | (414)5060917510004(7040)5KFX                                       |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49                            | (8006)054123450000130201(22)AJY3095721P49                          |
	  | https://id.gs1.org/gsrn/411369833821910265/srin/4003812                                 | (8018)411369833821910265(8019)4003812                              |
	  | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                                       | (401)0541234537290A41&GHpp33                                       |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/ser/72292641703            | (8006)054123450000130201(22)AJY3095721P49(21)72292641703           |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123/ser/72292641703 | (8006)054123450000130201(22)AJY3095721P49(10)ABC123(21)72292641703 |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123/ser/72292641703                   | (8006)054123450000130201(10)ABC123(21)72292641703                  |
	  | https://id.gs1.org/gmn/5060917511473062B98T                                             | (8013)5060917511473062B98T                                         |
	  | https://id.gs1.org/gln/5060917510004/glnx/RP56J920471                                   | (414)5060917510004(254)RP56J920471                                 |
	  | https://id.gs1.org/gtin/05412345000013                                                  | (01)05412345000013                                                 |
	  | https://id.gs1.org/billTo/5060917510004                                                 | (411)5060917510004                                                 |
	  | https://id.gs1.org/party/5060917510004/7040/5KFX                                        | (417)5060917510004(7040)5KFX                                       |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33/7040/5BVR                             | (8004)0541234537290A41&GHpp33(7040)5BVR                            |
	  | https://id.gs1.org/gsin/21173492661712079                                               | (402)21173492661712079                                             |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123                 | (8006)054123450000130201(22)AJY3095721P49(10)ABC123                |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123/ser/72292641703                       | (01)05412345000013(10)ABC123(21)72292641703                        |
	  | https://id.gs1.org/payTo/5060917510004                                                  | (415)5060917510004                                                 |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33/cpsn/6639047221                         | (8010)0541234537290A41#GH33(8011)6639047221                        |
	  | https://id.gs1.org/sscc/998440410914660971                                              | (00)998440410914660971                                             |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123                                       | (01)05412345000013(10)ABC123                                       |
	  | https://id.gs1.org/shipFor/5060917510004                                                | (413)5060917510004                                                 |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/ser/72292641703                | (01)05412345000013(22)AJY3095721P49(21)72292641703                 |
	  | https://id.gs1.org/glnProd/5060917510004                                                | (416)5060917510004                                                 |
	  | https://id.gs1.org/itip/054123450000130201/ser/72292641703                              | (8006)054123450000130201(21)72292641703                            |
	  | https://id.gs1.org/purchasedFrom/5060917510004                                          | (412)5060917510004                                                 |
	  | https://id.gs1.org/shipTo/5060917510004                                                 | (410)5060917510004                                                 |
	  | https://id.gs1.org/gsrnp/817019610850151481/srin/4003812                                | (8017)817019610850151481(8019)4003812                              |

Scenario Outline: Convert a Digital Link for a medicinal identifier to an element string
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                             | elementStrings                                    |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703           | 01054123450000132172292641703<GS>10ABC123         |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703?17=290331 | 0105412345000013172903312172292641703<GS>10ABC123 |

Scenario Outline: Convert a Digital Link for a medicinal identifier to an element string with brackets
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string with brackets
    Then the element string should be "<elementStrings>"

    Examples:
	  | digitalLink                                                             | elementStrings                                        |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703           | (01)05412345000013(10)ABC123(21)72292641703           |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703?17=290331 | (01)05412345000013(10)ABC123(21)72292641703(17)290331 |

Scenario Outline: Convert a partially compressed Digital Link to an element string
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string
    Then the element string should be "<elementStrings>"

    Examples:
	 | digitalLink                                                                 | elementStrings                                                         |
	 | https://id.gs1.org/8017/817019610850151481/gBlz0X5A                         | 80194003812<GS>8017817019610850151481                                  |
	 | https://id.gs1.org/8010/0541234537290A41%23GH33/gBGmLt9I1                   | 80116639047221<GS>80100541234537290A41#GH33                            |
	 | https://id.gs1.org/416/5060917510004                                        | 4165060917510004                                                       |
	 | https://id.gs1.org/417/5060917510004/cEBWUKu                                | 417506091751000470405KFX                                               |
	 | https://id.gs1.org/8010/0541234537290A41%23GH33                             | 80100541234537290A41#GH33                                              |
	 | https://id.gs1.org/01/05412345000013/I1j3FSNWcOGeFywaR5CZm                  | 01054123450000132358TFV883a904GH&3                                     |
	 | https://id.gs1.org/01/5412345000013/I1j3FSNWcOGeFywaR5CZm                   | 01054123450000132358TFV883a904GH&3                                     |
	 | https://id.gs1.org/417/5060917510004                                        | 4175060917510004                                                       |
	 | https://id.gs1.org/8004/0541234537290A41%26GHpp33                           | 80040541234537290A41&GHpp33                                            |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSM                          | 10ABC123<GS>8006054123450000130201                                     |
	 | https://id.gs1.org/253/3060077601309                                        | 2533060077601309                                                       |
	 | https://id.gs1.org/8004/0541234537290A41%26GHpp33/cEB2Cqi                   | 70407BVR<GS>80040541234537290A41&GHpp33                                |
	 | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V04                       | 010541234500001310ABC123<GS>2172292641703                              |
	 | https://id.gs1.org/402/21173492661712079                                    | 40221173492661712079                                                   |
	 | https://id.gs1.org/01/05412345000013                                        | 0105412345000013                                                       |
	 | https://id.gs1.org/255/5678409730535                                        | 2555678409730535                                                       |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V05E2gEsb6e897an8eg | 10ABC123<GS>2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201 |
	 | https://id.gs1.org/414/5060917510004/JUa0T-eifdtOO9Q                        | 4145060917510004254RP56J920471                                         |
	 | https://id.gs1.org/8018/411369833821910265                                  | 8018411369833821910265                                                 |
	 | https://id.gs1.org/410/5060917510004                                        | 4105060917510004                                                       |
	 | https://id.gs1.org/415/5060917510004                                        | 4155060917510004                                                       |
	 | https://id.gs1.org/01/05412345000013/Im0AljfT3nvbU_j0                       | 010541234500001322AJY3095721P49                                        |
	 | https://id.gs1.org/414/5060917510004/cEBWUKu                                | 414506091751000470405KFX                                               |
	 | https://id.gs1.org/8006/054123450000130201/IQuGp9FdO                        | 2172292641703<GS>8006054123450000130201                                |
	 | https://id.gs1.org/8006/054123450000130201                                  | 8006054123450000130201                                                 |
	 | https://id.gs1.org/01/05412345000013/IQuGp9FdO                              | 01054123450000132172292641703                                          |
	 | https://id.gs1.org/8018/411369833821910265/gBlz0X5A                         | 80194003812<GS>8018411369833821910265                                  |
	 | https://id.gs1.org/01/05412345000013/EEarwSMibQCWN9Pee9tT-PQ                | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	 | https://id.gs1.org/01/05412345000013/IQuGp9FdORNoBLG-nvPe2p_Ho              | 01054123450000132172292641703<GS>22AJY3095721P49                       |
	 | https://id.gs1.org/414/5060917510004                                        | 4145060917510004                                                       |
	 | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V05E2gEsb6e897an8eg       | 010541234500001310ABC123<GS>2172292641703<GS>22AJY3095721P49           |
	 | https://id.gs1.org/412/5060917510004                                        | 4125060917510004                                                       |
	 | https://id.gs1.org/413/5060917510004                                        | 4135060917510004                                                       |
	 | https://id.gs1.org/8006/054123450000130201/IQuGp9FdORNoBLG-nvPe2p_Ho        | 2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201             |
	 | https://id.gs1.org/8013/5060917511473062B98T                                | 80135060917511473062B98T                                               |
	 | https://id.gs1.org/8017/817019610850151481                                  | 8017817019610850151481                                                 |
	 | https://id.gs1.org/401/0541234537290A41%26GHpp33                            | 4010541234537290A41&GHpp33                                             |
	 | https://id.gs1.org/8006/054123450000130201/Im0AljfT3nvbU_j0                 | 22AJY3095721P49<GS>8006054123450000130201                              |
	 | https://id.gs1.org/411/5060917510004                                        | 4115060917510004                                                       |
	 | https://id.gs1.org/01/05412345000013/EEarwSM                                | 010541234500001310ABC123                                               |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMibQCWN9Pee9tT-PQ          | 10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201                  |
	 | https://id.gs1.org/00/998440410914660971                                    | 00998440410914660971                                                   |
	 | https://id.gs1.org/8003/04965031954585                                      | 800304965031954585                                                     |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V04                 | 10ABC123<GS>2172292641703<GS>8006054123450000130201                    |

Scenario Outline: Convert a partially compressed Digital Link to an element string with brackets
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string with brackets
    Then the element string should be "<elementStrings>"

    Examples:
	 | digitalLink                                                                 | elementStrings                                                     |
	 | https://id.gs1.org/01/05412345000013                                        | (01)05412345000013                                                 |
	 | https://id.gs1.org/01/5412345000013                                         | (01)05412345000013                                                 |
	 | https://id.gs1.org/413/5060917510004                                        | (413)5060917510004                                                 |
	 | https://id.gs1.org/8004/0541234537290A41%26GHpp33                           | (8004)0541234537290A41&GHpp33                                      |
	 | https://id.gs1.org/8006/054123450000130201/IQuGp9FdO                        | (8006)054123450000130201(21)72292641703                            |
	 | https://id.gs1.org/8010/0541234537290A41%23GH33                             | (8010)0541234537290A41#GH33                                        |
	 | https://id.gs1.org/8006/054123450000130201/Im0AljfT3nvbU_j0                 | (8006)054123450000130201(22)AJY3095721P49                          |
	 | https://id.gs1.org/414/5060917510004/cEBWUKu                                | (414)5060917510004(7040)5KFX                                       |
	 | https://id.gs1.org/411/5060917510004                                        | (411)5060917510004                                                 |
	 | https://id.gs1.org/8018/411369833821910265                                  | (8018)411369833821910265                                           |
	 | https://id.gs1.org/414/5060917510004                                        | (414)5060917510004                                                 |
	 | https://id.gs1.org/01/05412345000013/EEarwSMibQCWN9Pee9tT-PQ                | (01)05412345000013(22)AJY3095721P49(10)ABC123                      |
	 | https://id.gs1.org/416/5060917510004                                        | (416)5060917510004                                                 |
	 | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V04                       | (01)05412345000013(10)ABC123(21)72292641703                        |
	 | https://id.gs1.org/417/5060917510004/cEBWUKu                                | (417)5060917510004(7040)5KFX                                       |
	 | https://id.gs1.org/8017/817019610850151481/gBlz0X5A                         | (8017)817019610850151481(8019)4003812                              |
	 | https://id.gs1.org/410/5060917510004                                        | (410)5060917510004                                                 |
	 | https://id.gs1.org/401/0541234537290A41%26GHpp33                            | (401)0541234537290A41&GHpp33                                       |
	 | https://id.gs1.org/412/5060917510004                                        | (412)5060917510004                                                 |
	 | https://id.gs1.org/8006/054123450000130201                                  | (8006)054123450000130201                                           |
	 | https://id.gs1.org/8017/817019610850151481                                  | (8017)817019610850151481                                           |
	 | https://id.gs1.org/8006/054123450000130201/IQuGp9FdORNoBLG-nvPe2p_Ho        | (8006)054123450000130201(22)AJY3095721P49(21)72292641703           |
	 | https://id.gs1.org/01/05412345000013/IQuGp9FdO                              | (01)05412345000013(21)72292641703                                  |
	 | https://id.gs1.org/00/998440410914660971                                    | (00)998440410914660971                                             |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMibQCWN9Pee9tT-PQ          | (8006)054123450000130201(22)AJY3095721P49(10)ABC123                |
	 | https://id.gs1.org/253/3060077601309                                        | (253)3060077601309                                                 |
	 | https://id.gs1.org/01/05412345000013/IQuGp9FdORNoBLG-nvPe2p_Ho              | (01)05412345000013(22)AJY3095721P49(21)72292641703                 |
	 | https://id.gs1.org/8003/04965031954585                                      | (8003)04965031954585                                               |
	 | https://id.gs1.org/414/5060917510004/JUa0T-eifdtOO9Q                        | (414)5060917510004(254)RP56J920471                                 |
	 | https://id.gs1.org/01/05412345000013/Im0AljfT3nvbU_j0                       | (01)05412345000013(22)AJY3095721P49                                |
	 | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V05E2gEsb6e897an8eg       | (01)05412345000013(22)AJY3095721P49(10)ABC123(21)72292641703       |
	 | https://id.gs1.org/01/05412345000013/I1j3FSNWcOGeFywaR5CZm                  | (01)05412345000013(235)8TFV883a904GH&3                             |
	 | https://id.gs1.org/255/5678409730535                                        | (255)5678409730535                                                 |
	 | https://id.gs1.org/8010/0541234537290A41%23GH33/gBGmLt9I1                   | (8010)0541234537290A41#GH33(8011)6639047221                        |
	 | https://id.gs1.org/417/5060917510004                                        | (417)5060917510004                                                 |
	 | https://id.gs1.org/01/05412345000013/EEarwSM                                | (01)05412345000013(10)ABC123                                       |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V05E2gEsb6e897an8eg | (8006)054123450000130201(22)AJY3095721P49(10)ABC123(21)72292641703 |
	 | https://id.gs1.org/8018/411369833821910265/gBlz0X5A                         | (8018)411369833821910265(8019)4003812                              |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V04                 | (8006)054123450000130201(10)ABC123(21)72292641703                  |
	 | https://id.gs1.org/415/5060917510004                                        | (415)5060917510004                                                 |
	 | https://id.gs1.org/8004/0541234537290A41%26GHpp33/cEB2Cqi                   | (8004)0541234537290A41&GHpp33(7040)7BVR                            |
	 | https://id.gs1.org/402/21173492661712079                                    | (402)21173492661712079                                             |
	 | https://id.gs1.org/8013/5060917511473062B98T                                | (8013)5060917511473062B98T                                         |
	 | https://id.gs1.org/8006/054123450000130201/EEarwSM                          | (8006)054123450000130201(10)ABC123                                 |

Scenario Outline: Convert a compressed Digital Link to an element string
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string
    Then the element string should be "<elementStrings>"

    Examples:
	 | digitalLink                                                     | elementStrings                                                         |
	 | https://id.gs1.org/QTSaVjC_dA                                   | 4135060917510004                                                       |
	 | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2Y                | 80100541234537290A41#GH33                                              |
	 | https://id.gs1.org/AQnYUc1gmkTaASxvp7z3tqfx6                    | 010541234500001322AJY3095721P49                                        |
	 | https://id.gs1.org/cEB2CqjAAkuwatGLJm0as27JywgtGKaPI4cGbM       | 70407BVR<GS>80040541234537290A41&GHpp33                                |
	 | https://id.gs1.org/QSSaVjC_dA                                   | 4125060917510004                                                       |
	 | https://id.gs1.org/AQnYUc1gmkIXDU-iun                           | 01054123450000132172292641703                                          |
	 | https://id.gs1.org/QCJZyW9mC6Z4                                 | 40221173492661712079                                                   |
	 | https://id.gs1.org/JVUqG5hJ5wA                                  | 2555678409730535                                                       |
	 | https://id.gs1.org/QBl2DVoxZM2jVm3ZOWEFoxTR5HDgzZg              | 4010541234537290A41&GHpp33                                             |
	 | https://id.gs1.org/QXSaVjC_dHBAVlCrg                            | 417506091751000470405KFX                                               |
	 | https://id.gs1.org/JTLIetaiHYA                                  | 2533060077601309                                                       |
	 | https://id.gs1.org/JUa0T-eifdtOO9UFEmlYwv3Q                     | 4145060917510004254RP56J920471                                         |
	 | https://id.gs1.org/QUSaVjC_dHBAVlCrg                            | 414506091751000470405KFX                                               |
	 | https://id.gs1.org/gAYMBI_bNGwJk                                | 8006054123450000130201                                                 |
	 | https://id.gs1.org/QQSaVjC_dA                                   | 4105060917510004                                                       |
	 | https://id.gs1.org/EEarwSMhC4an0V08ADBgJH7Zo2BMg                | 10ABC123<GS>2172292641703<GS>8006054123450000130201                    |
	 | https://id.gs1.org/EEarwSOABgwEj9s0bAmQ                         | 10ABC123<GS>8006054123450000130201                                     |
	 | https://id.gs1.org/AQnYUc1gmg                                   | 0105412345000013                                                       |
	 | https://id.gs1.org/EEarwSMhC4an0V05E2gEsb6e897an8ewAMGAkftmjYEy | 10ABC123<GS>2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201 |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6c                    | 010541234500001310ABC123<GS>2172292641703                              |
	 | https://id.gs1.org/gASXYNWjFkzaNWbdk5YQWjFNHkcODNm              | 80040541234537290A41&GHpp33                                            |
	 | https://id.gs1.org/QVSaVjC_dA                                   | 4155060917510004                                                       |
	 | https://id.gs1.org/AQnYUc1gmkax7ipGrOHDPC5YNI8hMz               | 01054123450000132358TFV883a904GH&3                                     |
	 | https://id.gs1.org/AN2yxDhfaiaw                                 | 00998440410914660971                                                   |
	 | https://id.gs1.org/Im0AljfT3nvbU_j2ABgwEj9s0bAmQ                | 22AJY3095721P49<GS>8006054123450000130201                              |
	 | https://id.gs1.org/QWSaVjC_dA                                   | 4165060917510004                                                       |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JG                             | 010541234500001310ABC123                                               |
	 | https://id.gs1.org/gBe1ajAd9ATDmAGXPRfk                         | 8017817019610850151481<GS>80194003812                                  |
	 | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2cAI0xdvpGo       | 80100541234537290A41#GH33<GS>80116639047221                            |
	 | https://id.gs1.org/gBhbV6rzBENPk                                | 8018411369833821910265                                                 |
	 | https://id.gs1.org/gAMJCAXukTMA                                 | 800304965031954585                                                     |
	 | https://id.gs1.org/QUSaVjC_dA                                   | 4145060917510004                                                       |
	 | https://id.gs1.org/QRSaVjC_dA                                   | 4115060917510004                                                       |
	 | https://id.gs1.org/EEarwSMibQCWN9Pee9tT-PYAGDASP2zRsCZ          | 10ABC123<GS>22AJY3095721P49<GS>8006054123450000130201                  |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGRNoBLG-nvPe2p_Ho             | 010541234500001310ABC123<GS>22AJY3095721P49                            |
	 | https://id.gs1.org/IQuGp9FdORNoBLG-nvPe2p_HsADBgJH7Zo2BMg       | 2172292641703<GS>22AJY3095721P49<GS>8006054123450000130201             |
	 | https://id.gs1.org/gBN05060917511473062B98T                     | 80135060917511473062B98T                                               |
	 | https://id.gs1.org/gBhbV6rzBENPmAGXPRfk                         | 8018411369833821910265<GS>80194003812                                  |
	 | https://id.gs1.org/QXSaVjC_dA                                   | 4175060917510004                                                       |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6cibQCWN9Pee9tT-PQ    | 010541234500001310ABC123<GS>2172292641703<GS>22AJY3095721P49           |
	 | https://id.gs1.org/gBe1ajAd9ATDk                                | 8017817019610850151481                                                 |
	 | https://id.gs1.org/AQnYUc1gmkIXDU-iunIm0AljfT3nvbU_j0           | 01054123450000132172292641703<GS>22AJY3095721P49                       |
	 | https://id.gs1.org/IQuGp9FdPAAwYCR-2aNgTI                       | 2172292641703<GS>8006054123450000130201                                |

Scenario Outline: Convert a compressed Digital Link to an element string with brackets
    Given I want to convert the following Digital Link to an element string: "<digitalLink>"
    When I convert the Digital Link to an element string with brackets
    Then the element string should be "<elementStrings>"

    Examples:
	 | digitalLink                                                     | elementStrings                                                     |
	 | https://id.gs1.org/JVUqG5hJ5wA                                  | (255)5678409730535                                                 |
	 | https://id.gs1.org/QCJZyW9mC6Z4                                 | (402)21173492661712079                                             |
	 | https://id.gs1.org/QRSaVjC_dA                                   | (411)5060917510004                                                 |
	 | https://id.gs1.org/gBN05060917511473062B98T                     | (8013)5060917511473062B98T                                         |
	 | https://id.gs1.org/QXSaVjC_dA                                   | (417)5060917510004                                                 |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6cibQCWN9Pee9tT-PQ    | (01)05412345000013(22)AJY3095721P49(10)ABC123(21)72292641703       |
	 | https://id.gs1.org/AQnYUc1gmg                                   | (01)05412345000013                                                 |
	 | https://id.gs1.org/QBl2DVoxZM2jVm3ZOWEFoxTR5HDgzZg              | (401)0541234537290A41&GHpp33                                       |
	 | https://id.gs1.org/AN2yxDhfaiaw                                 | (00)998440410914660971                                             |
	 | https://id.gs1.org/AQnYUc1gmkax7ipGrOHDPC5YNI8hMz               | (01)05412345000013(235)8TFV883a904GH&3                             |
	 | https://id.gs1.org/JTLIetaiHYA                                  | (253)3060077601309                                                 |
	 | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2Y                | (8010)0541234537290A41#GH33                                        |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6c                    | (01)05412345000013(10)ABC123(21)72292641703                        |
	 | https://id.gs1.org/QVSaVjC_dA                                   | (415)5060917510004                                                 |
	 | https://id.gs1.org/QUSaVjC_dHBAVlCrg                            | (414)5060917510004(7040)5KFX                                       |
	 | https://id.gs1.org/Im0AljfT3nvbU_j2ABgwEj9s0bAmQ                | (8006)054123450000130201(22)AJY3095721P49                          |
	 | https://id.gs1.org/gBe1ajAd9ATDk                                | (8017)817019610850151481                                           |
	 | https://id.gs1.org/cEB2CqjAAkuwatGLJm0as27JywgtGKaPI4cGbM       | (8004)0541234537290A41&GHpp33(7040)7BVR                            |
	 | https://id.gs1.org/EEarwSOABgwEj9s0bAmQ                         | (8006)054123450000130201(10)ABC123                                 |
	 | https://id.gs1.org/QTSaVjC_dA                                   | (413)5060917510004                                                 |
	 | https://id.gs1.org/AQnYUc1gmkIXDU-iun                           | (01)05412345000013(21)72292641703                                  |
	 | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2cAI0xdvpGo       | (8010)0541234537290A41#GH33(8011)6639047221                        |
	 | https://id.gs1.org/EEarwSMhC4an0V05E2gEsb6e897an8ewAMGAkftmjYEy | (8006)054123450000130201(22)AJY3095721P49(10)ABC123(21)72292641703 |
	 | https://id.gs1.org/AQnYUc1gmkIXDU-iunIm0AljfT3nvbU_j0           | (01)05412345000013(22)AJY3095721P49(21)72292641703                 |
	 | https://id.gs1.org/EEarwSMibQCWN9Pee9tT-PYAGDASP2zRsCZ          | (8006)054123450000130201(22)AJY3095721P49(10)ABC123                |
	 | https://id.gs1.org/QSSaVjC_dA                                   | (412)5060917510004                                                 |
	 | https://id.gs1.org/gBhbV6rzBENPmAGXPRfk                         | (8018)411369833821910265(8019)4003812                              |
	 | https://id.gs1.org/QWSaVjC_dA                                   | (416)5060917510004                                                 |
	 | https://id.gs1.org/gAMJCAXukTMA                                 | (8003)04965031954585                                               |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JGRNoBLG-nvPe2p_Ho             | (01)05412345000013(22)AJY3095721P49(10)ABC123                      |
	 | https://id.gs1.org/IQuGp9FdORNoBLG-nvPe2p_HsADBgJH7Zo2BMg       | (8006)054123450000130201(22)AJY3095721P49(21)72292641703           |
	 | https://id.gs1.org/AQnYUc1gmiCNV4JG                             | (01)05412345000013(10)ABC123                                       |
	 | https://id.gs1.org/QUSaVjC_dA                                   | (414)5060917510004                                                 |
	 | https://id.gs1.org/gBhbV6rzBENPk                                | (8018)411369833821910265                                           |
	 | https://id.gs1.org/EEarwSMhC4an0V08ADBgJH7Zo2BMg                | (8006)054123450000130201(10)ABC123(21)72292641703                  |
	 | https://id.gs1.org/gAYMBI_bNGwJk                                | (8006)054123450000130201                                           |
	 | https://id.gs1.org/QXSaVjC_dHBAVlCrg                            | (417)5060917510004(7040)5KFX                                       |
	 | https://id.gs1.org/IQuGp9FdPAAwYCR-2aNgTI                       | (8006)054123450000130201(21)72292641703                            |
	 | https://id.gs1.org/gBe1ajAd9ATDmAGXPRfk                         | (8017)817019610850151481(8019)4003812                              |
	 | https://id.gs1.org/QQSaVjC_dA                                   | (410)5060917510004                                                 |
	 | https://id.gs1.org/JUa0T-eifdtOO9UFEmlYwv3Q                     | (414)5060917510004(254)RP56J920471                                 |
	 | https://id.gs1.org/gASXYNWjFkzaNWbdk5YQWjFNHkcODNm              | (8004)0541234537290A41&GHpp33                                      |
	 | https://id.gs1.org/AQnYUc1gmkTaASxvp7z3tqfx6                    | (01)05412345000013(22)AJY3095721P49                                |
