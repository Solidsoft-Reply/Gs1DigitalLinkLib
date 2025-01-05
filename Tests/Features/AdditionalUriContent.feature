Feature: AdditionalUriContent

Scenario: Digital Link with non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression 
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially compressed Digital Link with non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"

Scenario: Compressed Digital Link with non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"

Scenario: Digital Link with compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression 
	And compress other key-value pairs
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially compressed Digital Link with compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And compress other key-value pairs
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"

Scenario: Compressed Digital Link with compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression 
	And compress other key-value pairs
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"

Scenario: Digital Link with fragment specifier - compress non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And no compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331#chapter1"

Scenario: Partially compressed GS1 Digital Link with fragment specifier - compress non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And partial compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw#chapter1"

Scenario: Compressed Digital Link with fragment specifier - compress non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY#chapter1"

Scenario: Digital Link with fragment specifier and compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf#chapter1"

Scenario: Partially compressed Digital Link with fragment specifier and compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw#chapter1"

Scenario: Compressed GS1 Digital Link with fragment specifier and compressed non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4#chapter1"

Scenario: Digital Link from element string with non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression 
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially compressed Digital Link from element string with non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"

	
Scenario: Compressed Digital Link from element string with non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"

	Scenario: Digital Link from element string with compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression 
	And compress other key-value pairs
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially compressed Digital Link from element string with compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And compress other key-value pairs
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"

Scenario: Compressed Digital Link from element string with compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression 
	And compress other key-value pairs
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"

Scenario: Digital Link from element string with fragment specifier - compress non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And no compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331#chapter1"

Scenario: Partially compressed Digital Link from element string with fragment specifier - compress non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And partial compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw#chapter1"

Scenario: Compressed Digital Link from element string with fragment specifier - compress non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY#chapter1"

Scenario: Digital Link from element string with fragment specifier and compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf#chapter1"

Scenario: Partially compressed Digital Link from element string with  fragment specifier and compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw#chapter1"

Scenario: Compressed Digital Link from element string with fragment specifier and compressed non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And compress other key-value pairs
	And a fragment specifier, as follows: "chapter1"
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4#chapter1"

Scenario: Partially compress uncompressed Digital Link with non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"
	And partial compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"

Scenario: Compress uncompressed Digital Link with non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"
	And compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"

Scenario: Partially compress uncompressed Digital Link with compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"
	And partial compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"

Scenario: Compress uncompressed Digital Link with compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"
	And compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"

Scenario: Decompress partially compressed Digital Link with additional non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Decompress compressed Digital Link with additional non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially decompress compressed Digital Link with additional non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"
	And partial compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"

Scenario: Decompress partially compressed Digital Link with compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"
	And no compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Decompress compressed Digital Link with compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"
	And no compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially decompress compressed Digital Link with compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf"
	And partial compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"

Scenario: Decompress partially compressed Digital Link containing compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Decompress compressed Digital Link containing compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf"

Scenario: Partially decompress compressed Digital Link containing compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"
	And partial compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf"

Scenario: Partially decompress compressed Digital Link with compressed non-GS1 key-value pairs - specify compression of other key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4"
	And partial compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw"

Scenario: Compress uncompressed Digital Link with non key-value data
	Given the following Digital Link URI: "<digitalLinkUri>"
	And compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<compressedDigitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                       |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&someparameter&arv=true&anotherparameter&donor=bmgf | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter |

Scenario: Compress uncompressed Digital Link with non key-value data and no compression of non-GS1 key-value pairs
	Given the following Digital Link URI: "<digitalLinkUri>"
	And compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<compressedDigitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                    |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&someparameter&arv=true&anotherparameter&donor=bmgf | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter&anotherparameter |

Scenario: Decompress compressed Digital Link with non key-value data
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                       |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter |

Scenario: Decompress compressed Digital Link with non key-value data and uncompressed non-GS1 key-pair values
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                       |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter&anotherparameter |

Scenario: Partially decompress compressed Digital Link with non key-value data and compress non-GS1 key-value pairs
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And partial compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                          | compressedDigitalLinkUri                                                                       |
    | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                              |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter                  |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter&anotherparameter | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter |


Scenario: Partially decompress compressed Digital Link with non key-value data
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And partial compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                       | compressedDigitalLinkUri                                                                       |
    | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter                                           | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter                                              |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter                  |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter&anotherparameter | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter |

Scenario: Decompress partially compressed Digital Link with non key-value data and compressed non-GS1 key-value pairs
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                                |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter&anotherparameter |

Scenario: Decompress partially compressed Digital Link with non key-value data and uncompressed non-GS1 key-pair values
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                              | compressedDigitalLinkUri                                                                             |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter                                                | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter                  | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter&anotherparameter |

Scenario: Compress uncompressed Digital Link with non key-value data and fragment specifier
	Given the following Digital Link URI: "<digitalLinkUri>"
	And compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<compressedDigitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                                |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&someparameter&arv=true&anotherparameter&donor=bmgf#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter#chapter1 |

Scenario: Compress uncompressed Digital Link with non key-value data and no compression of non-GS1 key-value pairs and fragment specifier
	Given the following Digital Link URI: "<digitalLinkUri>"
	And compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<compressedDigitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                             |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                                         |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&someparameter&arv=true&anotherparameter&donor=bmgf#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 |

Scenario: Decompress compressed Digital Link with non key-value data and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                                |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                                            |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter#chapter1 |

Scenario: Decompress compressed Digital Link with non key-value data and uncompressed non-GS1 key-pair values and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                             |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                                         |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 |

Scenario: Partially decompress compressed Digital Link with non key-value data and compressed non-GS1 key-value pairs and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And partial compression
	And compress other key-value pairs
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                   | compressedDigitalLinkUri                                                                                |
    | https://id.gs1.org/01/05412345000013/EEarwSM#chapter1                                                            | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                                            |
    | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter#chapter1                                              | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter&anotherparameter#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter#chapter1 |


Scenario: Partially decompress compressed Digital Link with non key-value data and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And partial compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                | compressedDigitalLinkUri                                                                                |
    | https://id.gs1.org/01/05412345000013/EEarwSM#chapter1                                                         | https://id.gs1.org/AQnYUc1gmiCNV4JG#chapter1                                                            |
    | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter#chapter1                                           | https://id.gs1.org/AQnYUc1gmiCNV4JG?someparameter#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D4?someparameter&anotherparameter#chapter1 |

Scenario: Decompress partially compressed Digital Link with non key-value data and compressed non-GS1 key-value pairs and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                                         |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/01/05412345000013/EEarwSM#chapter1                                                            |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter#chapter1                                              |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | https://id.gs1.org/01/05412345000013/EEarwSMXRuG_BtV3sJbXc94V2ieithG5oHw?someparameter&anotherparameter#chapter1 |

Scenario: Decompress partially compressed Digital Link with non key-value data and uncompressed non-GS1 key-pair values and fragment specifier
	Given the following Digital Link URI: "<compressedDigitalLinkUri>"
	And no compression
	When I change the compression level of the GS1 Digital Link
	Then the result should be "<digitalLinkUri>"

	Examples:
    | digitalLinkUri                                                                                                       | compressedDigitalLinkUri                                                                                      |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | https://id.gs1.org/01/05412345000013/EEarwSM#chapter1                                                         |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | https://id.gs1.org/01/05412345000013/EEarwSM?someparameter#chapter1                                           |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter#chapter1                  |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 |


Scenario: Extract data from a GS1 Digital Link with non key-value data and fragment specifier
	Given the following Digital Link URI: "<digitalLinkUri>"
	When I extract data from the GS1 Digital Link
	Then the data result should be:
	| PropertyName            | Value                     |
	| Gs1AIs                  | <gs1AIs>                  |
	| NonGs1KeyValuePairs     | <nonGs1KeyValuePairs>     |
	| OtherQueryStringContent | <otherQueryStringContent> |
	| FragmentSpecifier       | <fragmentSpecifier>       |

	Examples:
    | digitalLinkUri                                                                                                       | gs1AIs                                              | nonGs1KeyValuePairs         | otherQueryStringContent        | fragmentSpecifier |
    | https://id.gs1.org/01/05412345000013/10/ABC123#chapter1                                                              | {"01":"05412345000013","10":"ABC123"}               |                             |                                | chapter1          |
    | https://id.gs1.org/01/05412345000013/10/ABC123?someparameter#chapter1                                                | {"01":"05412345000013","10":"ABC123"}               |                             | someparameter                  | chapter1          |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter#chapter1                  | {"01":"05412345000013","10":"ABC123","17":"290331"} | {"arv":true,"donor":"bmgf"} | someparameter                  | chapter1          |
    | https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&someparameter&anotherparameter#chapter1 | {"01":"05412345000013","10":"ABC123","17":"290331"} | {"arv":true,"donor":"bmgf"} | someparameter&anotherparameter | chapter1          |

Scenario: Optimised Digital Link with non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value           |
		| 01 | 05412345000013  |
		| 10 | ABC123          |
		| 17 | 290331          |
		| 21 | R759025244015BJ |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression
	And optimisation
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123/21/R759025244015BJ?17=290331&arv=true&donor=bmgf"

	Scenario: Optimised partially compressed Digital Link with non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value           |
		| 01 | 05412345000013  |
		| 10 | ABC123          |
		| 17 | 290331          |
		| 21 | R759025244015BJ |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And optimisation
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGyFvR759025244015BJ?arv=true&donor=bmgf"

Scenario: Optimised compressed Digital Link with non-GS1 key-value pairs
		  unoptimised: https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DZC3o98-6bc7ccaa8gkg?arv=true&donor=bmgf
		  optimised:   https://id.gs1.org/GgnYUc1gmo1Xgkbej3z7ptztxxpryCSjcNg?arv=true&donor=bmgf
	Given the following AIs:
		| AI | Value           |
		| 01 | 05412345000013  |
		| 10 | ABC123          |
		| 17 | 290331          |
		| 21 | R759025244015BJ |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And optimisation
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/GgnYUc1gmo1Xgkbej3z7ptztxxpryCSjcNg?arv=true&donor=bmgf"

Scenario: Optimised Digital Link from element string with non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331(21)R759025244015BJ"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression
	And optimisation
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123/21/R759025244015BJ?17=290331&arv=true&donor=bmgf"

	Scenario: Optimised partially compressed Digital Link from element string with non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331(21)R759025244015BJ"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And optimisation
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGyFvR759025244015BJ?arv=true&donor=bmgf"

Scenario: Optimised compressed Digital Link from element string with non-GS1 key-value pairs
		  unoptimised: https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DZC3o98-6bc7ccaa8gkg?arv=true&donor=bmgf
		  optimised:   https://id.gs1.org/GgnYUc1gmo1Xgkbej3z7ptztxxpryCSjcNg?arv=true&donor=bmgf
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331(21)R759025244015BJ"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And optimisation
	When I translate the element string to a Digital Link
	Then the result should be "https://id.gs1.org/GgnYUc1gmo1Xgkbej3z7ptztxxpryCSjcNg?arv=true&donor=bmgf"

Scenario: Digital Link with non-GS1 key-value pairs and other content
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And no compression
	And other query content: "abc=123&someotherparameter&xyz=321"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/10/ABC123?17=290331&arv=true&donor=bmgf&abc=123&xyz=321&someotherparameter"

Scenario: Partially compressed Digital Link with non-GS1 key-value pairs and other content
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And partial compression
	And other query content: "abc=123&someotherparameter&xyz=321"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/01/05412345000013/EEarwSMXRuGw?arv=true&donor=bmgf&abc=123&xyz=321&someotherparameter"

Scenario: Compressed Digital Link with non-GS1 key-value pairs and other content
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And other query content: "abc=123&someotherparameter&xyz=321"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3DY?arv=true&donor=bmgf&abc=123&xyz=321&someotherparameter"

	Scenario: Compressed Digital Link with compressed non-GS1 key-value pairs and other content
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| arv   | true  |
		| donor | bmgf  |
	And compression
	And compress other key-value pairs
	And other query content: "abc=123&someotherparameter&xyz=321"
	When I build a GS1 Digital Link
	Then the result should be "https://id.gs1.org/AQnYUc1gmiCNV4JGLo3Dfg2q72Etrue8K7RPRWwjc0D_g2m3ADHvweOWYBqC?someotherparameter"

Scenario: Digital Link from invalid (check digit) element string
	Given the following element string: "(01)05412345000012(10)ABC123(17)290331"
	When I translate the element string to a Digital Link
    Then an exception with message "The value 05412345000012 is invalid for AI 01." is thrown
	And an exception with message "The value 05412345000012 has an invalid checksum." is thrown
	And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from invalid (disallowed character) element string
	Given the following element string: "(01)0541234B000013(10)ABC123(17)290331"
	When I translate the element string to a Digital Link
    Then an exception with message "The value 0541234B000013 is invalid for AI 01." is thrown
	And an exception with message "The value 0541234B000013 does not match the specified pattern for the data element." is thrown
	And an exception with message "The value 0541234B000013 has an invalid checksum." is thrown
	And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from invalid (invalid date) element string
	Given the following element string: "(01)05412345000013(10)ABC123(17)294731"
	When I translate the element string to a Digital Link
    Then an exception with message "The value 294731 is invalid for AI 17." is thrown
	And an exception with message "The value 294731 does not match the specified pattern for the data element." is thrown
	And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from invalid data (invalid date)
	Given the following AIs:
		| AI  | Value              |
		| 01  | 05412345000013     |
		| 10  | ABC123             |
		| 17  | 290366             |
	When I build a GS1 Digital Link
    Then an exception with message "The value 290366 is invalid for AI 17." is thrown
    And an exception with message "The value 290366 does not match the specified pattern for the data element." is thrown
    And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from invalid data (check digit)
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000012 |
		| 10 | ABC123         |
		| 17 | 290331         |
	When I build a GS1 Digital Link
    Then an exception with message "The value 05412345000012 is invalid for AI 01." is thrown
    And an exception with message "The value 05412345000012 has an invalid checksum." is thrown
    And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from invalid data (no AI)
	Given the following AIs:
		| AI | Value          |
		| 10 | ABC123         |
		| 17 | 290331         |
	When I build a GS1 Digital Link
    Then an exception with message "No key identifier found in the GS1 DigitalLink URI path information." is thrown

Scenario: Digital Link from invalid data (invalid sequence)
	Given the following AIs:
		| AI  | Value              |
		| 01  | 05412345000013     |
		| 10  | ABC123             |
		| 17  | 290331             |
		| 22  | 054123450000130123 |
		| 235 | 7BRT873085HF7298   |
	When I build a GS1 Digital Link
    Then an exception with message "Invalid sequence of key qualifiers found in the GS1 DigitalLink URI path information." is thrown

Scenario: Digital Link from invalid (invalid sequence) element string
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331(22)054123450000130123(235)7BRT873085HF7298"
	When I translate the element string to a Digital Link
    Then an exception with message "Invalid sequence of key qualifiers found in the GS1 DigitalLink URI path information." is thrown

Scenario: Digital Link from invalid (invalid sequence) Digital Link
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123/22/054123450000130123/235/7BRT873085HF7298?17=290331"
	When I extract data from the GS1 Digital Link
    Then an exception with message "Invalid sequence of key qualifiers found in the GS1 DigitalLink URI path information." is thrown

Scenario: Digital Link from invalid (invalid sequence) Digital Link during compression
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123/22/054123450000130123/235/7BRT873085HF7298?17=290331"
	And partial compression
	When I change the compression level of the GS1 Digital Link
    Then an exception with message "Invalid sequence of key qualifiers found in the GS1 DigitalLink URI path information." is thrown

Scenario: Digital Link from invalid data
	Given the following Digital Link URI: "RY9V0U42Y325UR5723YY2YRWUIWERYWEWIBYRIHDYPW0NVEW9UT34YTYUTURIEWVUOIWETU"
	And no compression
	When I change the compression level of the GS1 Digital Link
    Then an exception with message "Unable to determine the form of the Digital Link." is thrown

Scenario: Digital Link from element string with invalid data
	Given the following element string: "RY9V0U42Y325UR5723YY2YRWUIWERYWEWIBYRIHDYPW0NVEW9UT34YTYUTURIEWVUOIWETU"
	When I translate the element string to a Digital Link
    Then an exception with message "Invalid application identifier RY." is thrown
    And an exception with message "The barcode contains invalid or unrecognised data." is thrown

Scenario: Digital Link from element string with MH10.8.2 data
	Given the following element string: "[)><RS>06<GS>9N110186865770<GS>1TABC123<GS>D290331<GS>SR759025244015BJ<RS><EOT>"
	When I translate the element string to a Digital Link
    Then an exception with message "The element string '[)>069N1101868657701TABC123D290331SR759025244015BJ' does not represent GS1 data." is thrown

Scenario: Digital Link with invalid non-GS1 key-value pairs
	Given the following AIs:
		| AI | Value          |
		| 01 | 05412345000013 |
		| 10 | ABC123         |
		| 17 | 290331         |
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| 999   | true  |
		| donor | bmgf  |
	And no compression 
	When I build a GS1 Digital Link
    Then an exception with message "'999=true' is invalid. Non-GS1 key values must not be all-numeric." is thrown

Scenario: Digital Link from element string with invalid non-GS1 key-value pairs
	Given the following element string: "(01)05412345000013(10)ABC123(17)290331(22)054123450000130123"
	And the following non-GS1 key-value pairs:
		| Key   | Value |
		| 999   | true  |
		| donor | bmgf  |
	When I translate the element string to a Digital Link
    Then an exception with message "'999=true' is invalid. Non-GS1 key values must not be all-numeric." is thrown

Scenario: Data from Digital Link string with invalid non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123/22/054123450000130123?17=290331&999=true&donor=bmgf"
	When I extract data from the GS1 Digital Link
    Then an exception with message "'999=true' is invalid. Non-GS1 key values must not be all-numeric." is thrown

Scenario: Analysis of Digital Link string with non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123/22/054123450000130123?17=290331&arv=true&donor=bmgf"
	When I analyse the GS1 Digital Link URI
	Then the analysis should contain:
	| PropertyName        | Value                                                        |
	| DetectedForm        | Uncompressed                                                 |
	| ElementStringOutput | (01)05412345000013(22)054123450000130123(10)ABC123(17)290331 |
	| PathComponents      | /01/05412345000013/10/ABC123/22/054123450000130123           |
	| QueryString         | 17=290331&arv=true&donor=bmgf                                |
	| UriStem             | https://id.gs1.org                                           |

Scenario: Semantic analysis of Digital Link string with non-GS1 key-value pairs
	Given the following Digital Link URI: "https://id.gs1.org/01/05412345000013/10/ABC123/22/054123450000130123?17=290331&arv=true&donor=bmgf"
	When I analyse the semantics of the GS1 Digital Link URI
	Then the semantic analysis should contain:
	| PropertyName               | Value                                                        |
	| gs1:gtin                   | 05412345000013                                               |
	| schema:gtin                | 05412345000013                                               |
	| gs1:hasBatchLot            | ABC123                                                       |
	| gs1:consumerProductVariant | 054123450000130123                                           |
	| gs1:expirationDate         | 2029-03-31                                                   |
	| gs1:elementStrings         | (01)05412345000013(22)054123450000130123(10)ABC123(17)290331 |
