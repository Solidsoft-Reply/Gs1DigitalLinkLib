Feature: CompressGs1DigitalLink

A short summary of the feature

Scenario Outline: Compress a decompressed Digital Link
    Given I have a decompressed Digital Link "<decompressedDigitalLink>"
    When I compress the decompressed Digital Link
    Then the compressed Digital Link should be "<expectedDigitalLink>"

    Examples:
	  | decompressedDigitalLink                                                              | expectedDigitalLink                                             |
	  | https://id.gs1.org/00/998440410914660971                                             | https://id.gs1.org/AN2yxDhfaiaw                                 |
	  | https://id.gs1.org/01/05412345000013                                                 | https://id.gs1.org/AQnYUc1gmg                                   |
	  | https://id.gs1.org/01/05412345000013/10/ABC123                                       | https://id.gs1.org/AQnYUc1gmiCNV4JG                             |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703                        | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6c                    |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123/21/72292641703       | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6cibQCWN9Pee9tT-PQ    |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123                      | https://id.gs1.org/AQnYUc1gmiCNV4JGRNoBLG-nvPe2p_Ho             |
	  | https://id.gs1.org/01/05412345000013/235/8TFV883a904GH%263                           | https://id.gs1.org/AQnYUc1gmkax7ipGrOHDPC5YNI8hMz               |
	  | https://id.gs1.org/01/05412345000013/21/72292641703                                  | https://id.gs1.org/AQnYUc1gmkIXDU-iun                           |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/21/72292641703                 | https://id.gs1.org/AQnYUc1gmkIXDU-iunIm0AljfT3nvbU_j0           |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49                                | https://id.gs1.org/AQnYUc1gmkTaASxvp7z3tqfx6                    |
	  | https://id.gs1.org/253/3060077601309                                                 | https://id.gs1.org/JTLIetaiHYA                                  |
	  | https://id.gs1.org/255/5678409730535                                                 | https://id.gs1.org/JVUqG5hJ5wA                                  |
	  | https://id.gs1.org/401/0541234537290A41%26GHpp33                                     | https://id.gs1.org/QBl2DVoxZM2jVm3ZOWEFoxTR5HDgzZg              |
	  | https://id.gs1.org/402/21173492661712079                                             | https://id.gs1.org/QCJZyW9mC6Z4                                 |
	  | https://id.gs1.org/410/5060917510004                                                 | https://id.gs1.org/QQSaVjC_dA                                   |
	  | https://id.gs1.org/411/5060917510004                                                 | https://id.gs1.org/QRSaVjC_dA                                   |
	  | https://id.gs1.org/412/5060917510004                                                 | https://id.gs1.org/QSSaVjC_dA                                   |
	  | https://id.gs1.org/413/5060917510004                                                 | https://id.gs1.org/QTSaVjC_dA                                   |
	  | https://id.gs1.org/414/5060917510004                                                 | https://id.gs1.org/QUSaVjC_dA                                   |
	  | https://id.gs1.org/414/5060917510004/254/RP56J920471                                 | https://id.gs1.org/JUa0T-eifdtOO9UFEmlYwv3Q                     |
	  | https://id.gs1.org/414/5060917510004/7040/5KFX                                       | https://id.gs1.org/QUSaVjC_dHBAVlCrg                            |
	  | https://id.gs1.org/415/5060917510004                                                 | https://id.gs1.org/QVSaVjC_dA                                   |
	  | https://id.gs1.org/416/5060917510004                                                 | https://id.gs1.org/QWSaVjC_dA                                   |
	  | https://id.gs1.org/417/5060917510004                                                 | https://id.gs1.org/QXSaVjC_dA                                   |
	  | https://id.gs1.org/417/5060917510004/7040/5KFX                                       | https://id.gs1.org/QXSaVjC_dHBAVlCrg                            |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33/7040/7BVR                          | https://id.gs1.org/cEB2CqjAAkuwatGLJm0as27JywgtGKaPI4cGbM       |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123/21/72292641703 | https://id.gs1.org/EEarwSMhC4an0V05E2gEsb6e897an8ewAMGAkftmjYEy |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123/21/72292641703                  | https://id.gs1.org/EEarwSMhC4an0V08ADBgJH7Zo2BMg                |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123                | https://id.gs1.org/EEarwSMibQCWN9Pee9tT-PYAGDASP2zRsCZ          |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123                                 | https://id.gs1.org/EEarwSOABgwEj9s0bAmQ                         |
	  | https://id.gs1.org/8003/04965031954585                                               | https://id.gs1.org/gAMJCAXukTMA                                 |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33                                    | https://id.gs1.org/gASXYNWjFkzaNWbdk5YQWjFNHkcODNm              |
	  | https://id.gs1.org/8006/054123450000130201                                           | https://id.gs1.org/gAYMBI_bNGwJk                                |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49                          | https://id.gs1.org/Im0AljfT3nvbU_j2ABgwEj9s0bAmQ                |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/21/72292641703           | https://id.gs1.org/IQuGp9FdORNoBLG-nvPe2p_HsADBgJH7Zo2BMg       |
	  | https://id.gs1.org/8006/054123450000130201/21/72292641703                            | https://id.gs1.org/IQuGp9FdPAAwYCR-2aNgTI                       |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33/8011/6639047221                      | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2cAI0xdvpGo       |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33                                      | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2Y                |
	  | https://id.gs1.org/8013/5060917511473062B98T                                         | https://id.gs1.org/gBN05060917511473062B98T                     |
	  | https://id.gs1.org/8017/817019610850151481                                           | https://id.gs1.org/gBe1ajAd9ATDk                                |
	  | https://id.gs1.org/8017/817019610850151481/8019/4003812                              | https://id.gs1.org/gBe1ajAd9ATDmAGXPRfk                         |
	  | https://id.gs1.org/8018/411369833821910265                                           | https://id.gs1.org/gBhbV6rzBENPk                                |
	  | https://id.gs1.org/8018/411369833821910265/8019/4003812                              | https://id.gs1.org/gBhbV6rzBENPmAGXPRfk                         |

Scenario Outline: Partially compress a decompressed Digital Link
    Given I have a decompressed Digital Link "<decompressedDigitalLink>"
    When I partially compress the decompressed Digital Link
    Then the compressed Digital Link should be "<expectedDigitalLink>"

    Examples:
	  | decompressedDigitalLink                                                              | expectedDigitalLink                                                         |
	  | https://id.gs1.org/00/998440410914660971                                             | https://id.gs1.org/00/998440410914660971                                    |
	  | https://id.gs1.org/01/05412345000013                                                 | https://id.gs1.org/01/05412345000013                                        |
	  | https://id.gs1.org/01/05412345000013/10/ABC123                                       | https://id.gs1.org/01/05412345000013/EEarwSM                                |
	  | https://id.gs1.org/01/05412345000013/10/ABC123/21/72292641703                        | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V04                       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123/21/72292641703       | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V05E2gEsb6e897an8eg       |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/10/ABC123                      | https://id.gs1.org/01/05412345000013/EEarwSMibQCWN9Pee9tT-PQ                |
	  | https://id.gs1.org/01/05412345000013/235/8TFV883a904GH%263                           | https://id.gs1.org/01/05412345000013/I1j3FSNWcOGeFywaR5CZm                  |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49                                | https://id.gs1.org/01/05412345000013/Im0AljfT3nvbU_j0                       |
	  | https://id.gs1.org/01/05412345000013/21/72292641703                                  | https://id.gs1.org/01/05412345000013/IQuGp9FdO                              |
	  | https://id.gs1.org/01/05412345000013/22/AJY3095721P49/21/72292641703                 | https://id.gs1.org/01/05412345000013/IQuGp9FdORNoBLG-nvPe2p_Ho              |
	  | https://id.gs1.org/253/3060077601309                                                 | https://id.gs1.org/253/3060077601309                                        |
	  | https://id.gs1.org/255/5678409730535                                                 | https://id.gs1.org/255/5678409730535                                        |
	  | https://id.gs1.org/401/0541234537290A41%26GHpp33                                     | https://id.gs1.org/401/0541234537290A41%26GHpp33                            |
	  | https://id.gs1.org/402/21173492661712079                                             | https://id.gs1.org/402/21173492661712079                                    |
	  | https://id.gs1.org/410/5060917510004                                                 | https://id.gs1.org/410/5060917510004                                        |
	  | https://id.gs1.org/411/5060917510004                                                 | https://id.gs1.org/411/5060917510004                                        |
	  | https://id.gs1.org/412/5060917510004                                                 | https://id.gs1.org/412/5060917510004                                        |
	  | https://id.gs1.org/413/5060917510004                                                 | https://id.gs1.org/413/5060917510004                                        |
	  | https://id.gs1.org/414/5060917510004                                                 | https://id.gs1.org/414/5060917510004                                        |
	  | https://id.gs1.org/414/5060917510004/7040/5KFX                                       | https://id.gs1.org/414/5060917510004/cEBWUKu                                |
	  | https://id.gs1.org/414/5060917510004/254/RP56J920471                                 | https://id.gs1.org/414/5060917510004/JUa0T-eifdtOO9Q                        |
	  | https://id.gs1.org/415/5060917510004                                                 | https://id.gs1.org/415/5060917510004                                        |
	  | https://id.gs1.org/416/5060917510004                                                 | https://id.gs1.org/416/5060917510004                                        |
	  | https://id.gs1.org/417/5060917510004                                                 | https://id.gs1.org/417/5060917510004                                        |
	  | https://id.gs1.org/417/5060917510004/7040/5KFX                                       | https://id.gs1.org/417/5060917510004/cEBWUKu                                |
	  | https://id.gs1.org/8003/04965031954585                                               | https://id.gs1.org/8003/04965031954585                                      |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33                                    | https://id.gs1.org/8004/0541234537290A41%26GHpp33                           |
	  | https://id.gs1.org/8004/0541234537290A41%26GHpp33/7040/7BVR                          | https://id.gs1.org/8004/0541234537290A41%26GHpp33/cEB2Cqi                   |
	  | https://id.gs1.org/8006/054123450000130201                                           | https://id.gs1.org/8006/054123450000130201                                  |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123                                 | https://id.gs1.org/8006/054123450000130201/EEarwSM                          |
	  | https://id.gs1.org/8006/054123450000130201/10/ABC123/21/72292641703                  | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V04                 |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123/21/72292641703 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V05E2gEsb6e897an8eg |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/10/ABC123                | https://id.gs1.org/8006/054123450000130201/EEarwSMibQCWN9Pee9tT-PQ          |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49                          | https://id.gs1.org/8006/054123450000130201/Im0AljfT3nvbU_j0                 |
	  | https://id.gs1.org/8006/054123450000130201/21/72292641703                            | https://id.gs1.org/8006/054123450000130201/IQuGp9FdO                        |
	  | https://id.gs1.org/8006/054123450000130201/22/AJY3095721P49/21/72292641703           | https://id.gs1.org/8006/054123450000130201/IQuGp9FdORNoBLG-nvPe2p_Ho        |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33                                      | https://id.gs1.org/8010/0541234537290A41%23GH33                             |
	  | https://id.gs1.org/8010/0541234537290A41%23GH33/8011/6639047221                      | https://id.gs1.org/8010/0541234537290A41%23GH33/gBGmLt9I1                   |
	  | https://id.gs1.org/8013/5060917511473062B98T                                         | https://id.gs1.org/8013/5060917511473062B98T                                    |
	  | https://id.gs1.org/8017/817019610850151481                                           | https://id.gs1.org/8017/817019610850151481                                  |
	  | https://id.gs1.org/8017/817019610850151481/8019/4003812                              | https://id.gs1.org/8017/817019610850151481/gBlz0X5A                         |
	  | https://id.gs1.org/8018/411369833821910265                                           | https://id.gs1.org/8018/411369833821910265                                  |
	  | https://id.gs1.org/8018/411369833821910265/8019/4003812                              | https://id.gs1.org/8018/411369833821910265/gBlz0X5A                         |

Scenario Outline: Compress a decompressed Digital Link with short names
    Given I have a decompressed Digital Link "<decompressedDigitalLink>"
    When I compress the decompressed Digital Link with short names
    Then the compressed Digital Link should be "<expectedDigitalLink>"

    Examples:
	  | decompressedDigitalLink                                                                 | expectedDigitalLink                                             |
	  | https://id.gs1.org/sscc/998440410914660971                                              | https://id.gs1.org/AN2yxDhfaiaw                                 |
	  | https://id.gs1.org/gtin/05412345000013                                                  | https://id.gs1.org/AQnYUc1gmg                                   |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123                                       | https://id.gs1.org/AQnYUc1gmiCNV4JG                             |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123/ser/72292641703                       | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6c                    |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123/ser/72292641703     | https://id.gs1.org/AQnYUc1gmiCNV4JGQhcNT6K6cibQCWN9Pee9tT-PQ    |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123                     | https://id.gs1.org/AQnYUc1gmiCNV4JGRNoBLG-nvPe2p_Ho             |
	  | https://id.gs1.org/gtin/05412345000013/235/8TFV883a904GH%263                            | https://id.gs1.org/AQnYUc1gmkax7ipGrOHDPC5YNI8hMz               |
	  | https://id.gs1.org/gtin/05412345000013/ser/72292641703                                  | https://id.gs1.org/AQnYUc1gmkIXDU-iun                           |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/ser/72292641703                | https://id.gs1.org/AQnYUc1gmkIXDU-iunIm0AljfT3nvbU_j0           |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49                                | https://id.gs1.org/AQnYUc1gmkTaASxvp7z3tqfx6                    |
	  | https://id.gs1.org/gdti/3060077601309                                                   | https://id.gs1.org/JTLIetaiHYA                                  |
	  | https://id.gs1.org/gcn/5678409730535                                                    | https://id.gs1.org/JVUqG5hJ5wA                                  |
	  | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                                       | https://id.gs1.org/QBl2DVoxZM2jVm3ZOWEFoxTR5HDgzZg              |
	  | https://id.gs1.org/gsin/21173492661712079                                               | https://id.gs1.org/QCJZyW9mC6Z4                                 |
	  | https://id.gs1.org/shipTo/5060917510004                                                 | https://id.gs1.org/QQSaVjC_dA                                   |
	  | https://id.gs1.org/billTo/5060917510004                                                 | https://id.gs1.org/QRSaVjC_dA                                   |
	  | https://id.gs1.org/purchasedFrom/5060917510004                                          | https://id.gs1.org/QSSaVjC_dA                                   |
	  | https://id.gs1.org/shipFor/5060917510004                                                | https://id.gs1.org/QTSaVjC_dA                                   |
	  | https://id.gs1.org/gln/5060917510004                                                    | https://id.gs1.org/QUSaVjC_dA                                   |
	  | https://id.gs1.org/gln/5060917510004/glnx/RP56J920471                                   | https://id.gs1.org/JUa0T-eifdtOO9UFEmlYwv3Q                     |
	  | https://id.gs1.org/gln/5060917510004/7040/5KFX                                          | https://id.gs1.org/QUSaVjC_dHBAVlCrg                            |
	  | https://id.gs1.org/payTo/5060917510004                                                  | https://id.gs1.org/QVSaVjC_dA                                   |
	  | https://id.gs1.org/glnProd/5060917510004                                                | https://id.gs1.org/QWSaVjC_dA                                   |
	  | https://id.gs1.org/party/5060917510004                                                  | https://id.gs1.org/QXSaVjC_dA                                   |
	  | https://id.gs1.org/party/5060917510004/7040/5KFX                                        | https://id.gs1.org/QXSaVjC_dHBAVlCrg                            |
	  | https://id.gs1.org/grai/04965031954585                                                  | https://id.gs1.org/gAMJCAXukTMA                                 |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33                                       | https://id.gs1.org/gASXYNWjFkzaNWbdk5YQWjFNHkcODNm              |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33/7040/7BVR                             | https://id.gs1.org/cEB2CqjAAkuwatGLJm0as27JywgtGKaPI4cGbM       |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123/ser/72292641703 | https://id.gs1.org/EEarwSMhC4an0V05E2gEsb6e897an8ewAMGAkftmjYEy |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123/ser/72292641703                   | https://id.gs1.org/EEarwSMhC4an0V08ADBgJH7Zo2BMg                |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123                 | https://id.gs1.org/EEarwSMibQCWN9Pee9tT-PYAGDASP2zRsCZ          |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123                                   | https://id.gs1.org/EEarwSOABgwEj9s0bAmQ                         |
	  | https://id.gs1.org/itip/054123450000130201                                              | https://id.gs1.org/gAYMBI_bNGwJk                                |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49                            | https://id.gs1.org/Im0AljfT3nvbU_j2ABgwEj9s0bAmQ                |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/ser/72292641703            | https://id.gs1.org/IQuGp9FdORNoBLG-nvPe2p_HsADBgJH7Zo2BMg       |
	  | https://id.gs1.org/itip/054123450000130201/ser/72292641703                              | https://id.gs1.org/IQuGp9FdPAAwYCR-2aNgTI                       |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33/cpsn/6639047221                         | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2cAI0xdvpGo       |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33                                         | https://id.gs1.org/gBCVYNWjFkzaNWbdk5YQWjFHHkM2Y                |
	  | https://id.gs1.org/gmn/5060917511473062B98T                                             | https://id.gs1.org/gBN05060917511473062B98T                     |
	  | https://id.gs1.org/gsrnp/817019610850151481                                             | https://id.gs1.org/gBe1ajAd9ATDk                                |
	  | https://id.gs1.org/gsrnp/817019610850151481/srin/4003812                                | https://id.gs1.org/gBe1ajAd9ATDmAGXPRfk                         |
	  | https://id.gs1.org/gsrn/411369833821910265                                              | https://id.gs1.org/gBhbV6rzBENPk                                |
	  | https://id.gs1.org/gsrn/411369833821910265/srin/4003812                                 | https://id.gs1.org/gBhbV6rzBENPmAGXPRfk                         |

Scenario Outline: Partially compress a decompressed Digital Link with short names
    Given I have a decompressed Digital Link "<decompressedDigitalLink>"
    When I partially compress the decompressed Digital Link
    Then the compressed Digital Link should be "<expectedDigitalLink>"

    Examples:
	  | decompressedDigitalLink                                                                 | expectedDigitalLink                                                         |
	  | https://id.gs1.org/sscc/998440410914660971                                              | https://id.gs1.org/00/998440410914660971                                    |
	  | https://id.gs1.org/gtin/05412345000013                                                  | https://id.gs1.org/01/05412345000013                                        |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123                                       | https://id.gs1.org/01/05412345000013/EEarwSM                                |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123/ser/72292641703                       | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V04                       |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123/ser/72292641703     | https://id.gs1.org/01/05412345000013/EEarwSMhC4an0V05E2gEsb6e897an8eg       |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123                     | https://id.gs1.org/01/05412345000013/EEarwSMibQCWN9Pee9tT-PQ                |
	  | https://id.gs1.org/gtin/05412345000013/235/8TFV883a904GH%263                            | https://id.gs1.org/01/05412345000013/I1j3FSNWcOGeFywaR5CZm                  |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49                                | https://id.gs1.org/01/05412345000013/Im0AljfT3nvbU_j0                       |
	  | https://id.gs1.org/gtin/05412345000013/ser/72292641703                                  | https://id.gs1.org/01/05412345000013/IQuGp9FdO                              |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/ser/72292641703                | https://id.gs1.org/01/05412345000013/IQuGp9FdORNoBLG-nvPe2p_Ho              |
	  | https://id.gs1.org/gdti/3060077601309                                                   | https://id.gs1.org/253/3060077601309                                        |
	  | https://id.gs1.org/gcn/5678409730535                                                    | https://id.gs1.org/255/5678409730535                                        |
	  | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                                       | https://id.gs1.org/401/0541234537290A41%26GHpp33                            |
	  | https://id.gs1.org/gsin/21173492661712079                                               | https://id.gs1.org/402/21173492661712079                                    |
	  | https://id.gs1.org/shipTo/5060917510004                                                 | https://id.gs1.org/410/5060917510004                                        |
	  | https://id.gs1.org/billTo/5060917510004                                                 | https://id.gs1.org/411/5060917510004                                        |
	  | https://id.gs1.org/purchasedFrom/5060917510004                                          | https://id.gs1.org/412/5060917510004                                        |
	  | https://id.gs1.org/shipFor/5060917510004                                                | https://id.gs1.org/413/5060917510004                                        |
	  | https://id.gs1.org/gln/5060917510004                                                    | https://id.gs1.org/414/5060917510004                                        |
	  | https://id.gs1.org/gln/5060917510004/7040/5KFX                                          | https://id.gs1.org/414/5060917510004/cEBWUKu                                |
	  | https://id.gs1.org/gln/5060917510004/glnx/RP56J920471                                   | https://id.gs1.org/414/5060917510004/JUa0T-eifdtOO9Q                        |
	  | https://id.gs1.org/payTo/5060917510004                                                  | https://id.gs1.org/415/5060917510004                                        |
	  | https://id.gs1.org/glnProd/5060917510004                                                | https://id.gs1.org/416/5060917510004                                        |
	  | https://id.gs1.org/party/5060917510004                                                  | https://id.gs1.org/417/5060917510004                                        |
	  | https://id.gs1.org/party/5060917510004/7040/5KFX                                        | https://id.gs1.org/417/5060917510004/cEBWUKu                                |
	  | https://id.gs1.org/grai/04965031954585                                                  | https://id.gs1.org/8003/04965031954585                                      |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33                                       | https://id.gs1.org/8004/0541234537290A41%26GHpp33                           |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33/7040/7BVR                             | https://id.gs1.org/8004/0541234537290A41%26GHpp33/cEB2Cqi                   |
	  | https://id.gs1.org/itip/054123450000130201                                              | https://id.gs1.org/8006/054123450000130201                                  |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123                                   | https://id.gs1.org/8006/054123450000130201/EEarwSM                          |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123/ser/72292641703                   | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V04                 |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123/ser/72292641703 | https://id.gs1.org/8006/054123450000130201/EEarwSMhC4an0V05E2gEsb6e897an8eg |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123                 | https://id.gs1.org/8006/054123450000130201/EEarwSMibQCWN9Pee9tT-PQ          |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49                            | https://id.gs1.org/8006/054123450000130201/Im0AljfT3nvbU_j0                 |
	  | https://id.gs1.org/itip/054123450000130201/ser/72292641703                              | https://id.gs1.org/8006/054123450000130201/IQuGp9FdO                        |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/ser/72292641703            | https://id.gs1.org/8006/054123450000130201/IQuGp9FdORNoBLG-nvPe2p_Ho        |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33                                         | https://id.gs1.org/8010/0541234537290A41%23GH33                             |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33/cpsn/6639047221                         | https://id.gs1.org/8010/0541234537290A41%23GH33/gBGmLt9I1                   |
	  | https://id.gs1.org/gmn/5060917511473062B98T                                             | https://id.gs1.org/8013/5060917511473062B98T                                |
	  | https://id.gs1.org/gsrnp/817019610850151481                                             | https://id.gs1.org/8017/817019610850151481                                  |
	  | https://id.gs1.org/gsrnp/817019610850151481/srin/4003812                                | https://id.gs1.org/8017/817019610850151481/gBlz0X5A                         |
	  | https://id.gs1.org/gsrn/411369833821910265                                              | https://id.gs1.org/8018/411369833821910265                                  |
	  | https://id.gs1.org/gsrn/411369833821910265/srin/4003812                                 | https://id.gs1.org/8018/411369833821910265/gBlz0X5A                         |

Scenario Outline: Partially compress decompressed Digital Link with short names to a Digital Link with short names
    Given I have a decompressed Digital Link "<decompressedDigitalLink>"
    When I partially compress the decompressed Digital Link to a Digital Link with short names
    Then the compressed Digital Link should be "<expectedDigitalLink>"

    Examples:
	  | decompressedDigitalLink                                                                 | expectedDigitalLink                                                         |
	  | https://id.gs1.org/sscc/998440410914660971                                              | https://id.gs1.org/sscc/998440410914660971                                  |
	  | https://id.gs1.org/gtin/05412345000013                                                  | https://id.gs1.org/gtin/05412345000013                                      |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123                                       | https://id.gs1.org/gtin/05412345000013/EEarwSM                              |
	  | https://id.gs1.org/gtin/05412345000013/lot/ABC123/ser/72292641703                       | https://id.gs1.org/gtin/05412345000013/EEarwSMhC4an0V04                     |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123/ser/72292641703     | https://id.gs1.org/gtin/05412345000013/EEarwSMhC4an0V05E2gEsb6e897an8eg     |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/lot/ABC123                     | https://id.gs1.org/gtin/05412345000013/EEarwSMibQCWN9Pee9tT-PQ              |
	  | https://id.gs1.org/gtin/05412345000013/235/8TFV883a904GH%263                            | https://id.gs1.org/gtin/05412345000013/I1j3FSNWcOGeFywaR5CZm                |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49                                | https://id.gs1.org/gtin/05412345000013/Im0AljfT3nvbU_j0                     |
	  | https://id.gs1.org/gtin/05412345000013/ser/72292641703                                  | https://id.gs1.org/gtin/05412345000013/IQuGp9FdO                            |
	  | https://id.gs1.org/gtin/05412345000013/cpv/AJY3095721P49/ser/72292641703                | https://id.gs1.org/gtin/05412345000013/IQuGp9FdORNoBLG-nvPe2p_Ho            |
	  | https://id.gs1.org/gdti/3060077601309                                                   | https://id.gs1.org/gdti/3060077601309                                       |
	  | https://id.gs1.org/gcn/5678409730535                                                    | https://id.gs1.org/gcn/5678409730535                                        |
	  | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                                       | https://id.gs1.org/ginc/0541234537290A41%26GHpp33                           |
	  | https://id.gs1.org/gsin/21173492661712079                                               | https://id.gs1.org/gsin/21173492661712079                                   |
	  | https://id.gs1.org/shipTo/5060917510004                                                 | https://id.gs1.org/shipTo/5060917510004                                     |
	  | https://id.gs1.org/billTo/5060917510004                                                 | https://id.gs1.org/billTo/5060917510004                                     |
	  | https://id.gs1.org/purchasedFrom/5060917510004                                          | https://id.gs1.org/purchasedFrom/5060917510004                              |
	  | https://id.gs1.org/shipFor/5060917510004                                                | https://id.gs1.org/shipFor/5060917510004                                    |
	  | https://id.gs1.org/gln/5060917510004                                                    | https://id.gs1.org/gln/5060917510004                                        |
	  | https://id.gs1.org/gln/5060917510004/7040/5KFX                                          | https://id.gs1.org/gln/5060917510004/cEBWUKu                                |
	  | https://id.gs1.org/gln/5060917510004/glnx/RP56J920471                                   | https://id.gs1.org/gln/5060917510004/JUa0T-eifdtOO9Q                        |
	  | https://id.gs1.org/payTo/5060917510004                                                  | https://id.gs1.org/payTo/5060917510004                                      |
	  | https://id.gs1.org/glnProd/5060917510004                                                | https://id.gs1.org/glnProd/5060917510004                                    |
	  | https://id.gs1.org/party/5060917510004                                                  | https://id.gs1.org/party/5060917510004                                      |
	  | https://id.gs1.org/party/5060917510004/7040/5KFX                                        | https://id.gs1.org/party/5060917510004/cEBWUKu                              |
	  | https://id.gs1.org/grai/04965031954585                                                  | https://id.gs1.org/grai/04965031954585                                      |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33                                       | https://id.gs1.org/giai/0541234537290A41%26GHpp33                           |
	  | https://id.gs1.org/giai/0541234537290A41%26GHpp33/7040/7BVR                             | https://id.gs1.org/giai/0541234537290A41%26GHpp33/cEB2Cqi                   |
	  | https://id.gs1.org/itip/054123450000130201                                              | https://id.gs1.org/itip/054123450000130201                                  |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123                                   | https://id.gs1.org/itip/054123450000130201/EEarwSM                          |
	  | https://id.gs1.org/itip/054123450000130201/lot/ABC123/ser/72292641703                   | https://id.gs1.org/itip/054123450000130201/EEarwSMhC4an0V04                 |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123/ser/72292641703 | https://id.gs1.org/itip/054123450000130201/EEarwSMhC4an0V05E2gEsb6e897an8eg |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/lot/ABC123                 | https://id.gs1.org/itip/054123450000130201/EEarwSMibQCWN9Pee9tT-PQ          |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49                            | https://id.gs1.org/itip/054123450000130201/Im0AljfT3nvbU_j0                 |
	  | https://id.gs1.org/itip/054123450000130201/ser/72292641703                              | https://id.gs1.org/itip/054123450000130201/IQuGp9FdO                        |
	  | https://id.gs1.org/itip/054123450000130201/cpv/AJY3095721P49/ser/72292641703            | https://id.gs1.org/itip/054123450000130201/IQuGp9FdORNoBLG-nvPe2p_Ho        |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33                                         | https://id.gs1.org/cpid/0541234537290A41%23GH33                             |
	  | https://id.gs1.org/cpid/0541234537290A41%23GH33/cpsn/6639047221                         | https://id.gs1.org/cpid/0541234537290A41%23GH33/gBGmLt9I1                   |
	  | https://id.gs1.org/gmn/5060917511473062B98T                                             | https://id.gs1.org/gmn/5060917511473062B98T                                 |
	  | https://id.gs1.org/gsrnp/817019610850151481                                             | https://id.gs1.org/gsrnp/817019610850151481                                 |
	  | https://id.gs1.org/gsrnp/817019610850151481/srin/4003812                                | https://id.gs1.org/gsrnp/817019610850151481/gBlz0X5A                        |
	  | https://id.gs1.org/gsrn/411369833821910265                                              | https://id.gs1.org/gsrn/411369833821910265                                  |
	  | https://id.gs1.org/gsrn/411369833821910265/srin/4003812                                 | https://id.gs1.org/gsrn/411369833821910265/gBlz0X5A                         |