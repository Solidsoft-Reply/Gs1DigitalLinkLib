Feature: ExtractFromGs1ElementStrings
  As a developer
  I want to verify that the ExtractFromGs1ElementStrings method correctly parses GS1 an element string
  So that I can ensure its reliability and correctness

  Scenario Outline: Parse valid bracketed GS1 an element string
    Given I have a bracketed GS1 element string "<elementString>"
    When I extract AIs and values
    Then the data should contain:
      | AI   | Value         |
      | <AI1> | <Value1>     |
      | <AI2> | <Value2>     |
      | <AI3> | <Value3>     |

    Examples:
      | elementString                                      | AI1 | Value1         | AI2  | Value2 | AI3  | Value3 |
      | (01)05412345000013(3103)000189(3923)2172(10)ABC123 | 01  | 05412345000013 | 3103 | 000189 | 3923 | 2172   |
      | (02)12345678901231(37)24(21)1234                   | 02  | 12345678901231 | 37   | 24     | 21   | 1234   |

  Scenario Outline: Parse valid unbracketed GS1 an element string
    Given I have an unbracketed GS1 element string "<elementString>"
    When I extract AIs and values
    Then the data should contain:
      | AI    | Value         |
      | <AI1> | <Value1>      |
      | <AI2> | <Value2>      |
      | <AI3> | <Value3>      |

    Examples:
      | elementString                                  | AI1  | Value1         | AI2  | Value2         | AI3 | Value3 |
      | 0105412345000013310300018939232172<GS>10ABC123 | 01   | 05412345000013 | 3103 | 000189         | 10  | ABC123 |
      | 31030001890105412345000013<GS>10XYZ            | 3103 | 000189         | 01   | 05412345000013 | 10  | XYZ    |

  Scenario: Parse GS1 element string with symbology identifiers
    Given I have a GS1 element string with symbology identifier "<elementString>"
    When I extract AIs and values
    Then the data should contain:
      | AI  | Value          |
      | 01  | 05412345000013 |
      | 10  | ABC123         |

    Examples:
      | elementString               |
      | ]C1010541234500001310ABC123  | 
      | ]C1(01)05412345000013(10)ABC123    |

  Scenario: Handle invalid AI value syntax (bracketed)
    Given I have a bracketed GS1 element string "(01)05412345000013(15)ABCDEF"
    When I extract AIs and values
    Then an exception with message "The value ABCDEF is invalid for AI 15" is thrown

  Scenario: Handle invalid AI value syntax (unbracketed)
    Given I have an unbracketed GS1 element string "010541234500001310ABC123<GS>15ABCDEF"
    When I extract AIs and values
    Then an exception with message "The value ABCDEF is invalid for AI 15." is thrown

  Scenario: Handle invalid AI value syntax (bracketed) without validation
    Given I have a bracketed GS1 element string "(01)05412345000013(15)ABCDEF"
    When I extract AIs and values without validation
    Then the data should contain:
      | AI  | Value          |
      | 01  | 05412345000013 |
      | 15  | ABCDEF         |

  Scenario: Handle invalid AI value syntax (unbracketed) without validation
    Given I have an unbracketed GS1 element string "010541234500001310ABC123<GS>15ABCDEF"
    When I extract AIs and values without validation
    Then the data should contain:
      | AI  | Value          |
      | 01  | 05412345000013 |
      | 10  | ABC123         |
      | 15  | ABCDEF         |

  Scenario: Handle unknown AI (bracketed)
    Given I have a bracketed GS1 element string "(49)991234567890"
    When I extract AIs and values
    Then an exception with message "Invalid application identifier 49." is thrown
    And an exception with message "The barcode contains invalid or unrecognised data." is thrown

  Scenario: Handle unknown AI (unbracketed)
    Given I have an unbracketed GS1 element string "49991234567890"
    When I extract AIs and values
    Then an exception with message "Invalid application identifier 49." is thrown
    And an exception with message "The barcode contains invalid or unrecognised data." is thrown

  Scenario: Parse GS1 element string with group separators
    Given I have an unbracketed GS1 element string "0105412345000013310300018939232172<GS>10ABC123"
    When I extract AIs and values
    Then the data should contain:
      | AI    | Value          |
      | 01    | 05412345000013 |
      | 3103  | 000189         |
      | 3923  | 2172           |
      | 10    | ABC123         |
