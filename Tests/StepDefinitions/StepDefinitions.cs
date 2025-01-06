using Newtonsoft.Json;
using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;
using Solidsoft.Reply.Gs1DigitalLinkLib;

using Reqnroll;

namespace Gs1DigitalLinkToolkitTests.StepDefinitions {

    [Binding]
    internal class StepDefinitions {
        private Dictionary<string, string>? _gs1Ais;
        private Dictionary<string, string>? _nonGs1KeyValuePairs;
        private string? _elementString = new(string.Empty);
        private string? _digitalLink = new(string.Empty);
        private Gs1DigitalLink? _digitalLinkObj = new(string.Empty);
        private Gs1DigitalLink? _decompressedDigitalLink = new(string.Empty);
        private Gs1Data? _dataResult = new();
        private string? _otherQueryContent = new(string.Empty);
        private string? _fragment = new(string.Empty);
        private DigitalLinkForm _digitalLinkForm;
        private CompressionLevel _compressionLevel;
        private bool _compressNonGs1KeyValuePairs;
        private bool _optimisation;
        private Exception? _thrownException = null;
        private static readonly object lockObject = LogFile.LockObject;
        private string _compressedDigitalLink = string.Empty;
        private int _inputColumnWidth = 88;
        private int _resultColumnWidth = 88;
        private Gs1ElementString _elementStringObj = new(string.Empty);
        private ScenarioContext _scenarioContext;
        private UriAnalytics _analytics;
        private UriSemantics _semanticAnalytics;

        public StepDefinitions(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to create a Digital Link for the GS1 element string ""(.*)""")]
        [Given(@"I want to create a Digital Link for the bracketed GS1 element string ""(.*)""")]
        [Given(@"I have a GS1 element string with symbology identifier ""(.*)""")]
        [Given(@"I want to create a Digital Link for a GS1 element string with symbology identifier ""(.*)""")]
        [Given(@"I want to create a compressed Digital Link for a GS1 element string with symbology identifier ""(.*)""")]
        [Given(@"I want to create a compressed Digital Link for the bracketed GS1 element string ""(.*)""")]
        [Given(@"I have a bracketed GS1 element string ""(.*)""")]
        public void CaptureElementString(string elementString) {
            _elementString = elementString;
        }

        [Given(@"the following AIs:")]
        public void GivenAGs1DigitalLinkWithTheFollowingAIs(Table table) {
            _gs1Ais = [];

            foreach (var row in table.Rows) {
                _gs1Ais.Add(row["AI"], row["Value"]);
            }
        }

        [Given(@"the following element string: ""(.*)""")]
        public void GivenTheFollowingElementString(string elementString) {
            _elementString = elementString.Replace("<GS>", "\u001D")
                                          .Replace("<RS>", "\u001E")
                                          .Replace("<EOT>", "\u0004");
        }

        [Given(@"the following non-GS1 key-value pairs:")]
        public void GivenAGs1DigitalLinkWithTheFollowingNonGs1KeyValuePairs(Table table) {
            _nonGs1KeyValuePairs = [];

            foreach (var row in table.Rows) {
                _nonGs1KeyValuePairs.Add(row["Key"], row["Value"]);
            }
        }

        [Given(@"no compression")]
        public void GivenNoCompressionOfUriWithAdditionalContent() {
            _digitalLinkForm = DigitalLinkForm.Uncompressed;
            _compressionLevel = CompressionLevel.Uncompressed;
        }

        [Given(@"partial compression")]
        public void GivenPartialCompressionOfUriWithAdditionalContent() {
            _digitalLinkForm = DigitalLinkForm.PartiallyCompressed;
            _compressionLevel = CompressionLevel.PartiallyCompressed;
        }

        [Given(@"compression")]
        public void GivenCompressionOfUriWithAdditionalContent() {
            _digitalLinkForm = DigitalLinkForm.Compressed;
            _compressionLevel = CompressionLevel.Compressed;
        }

        [Given(@"compress other key-value pairs")]
        public void GivenWeCompressOtherKeyValuePairs() {
            _compressNonGs1KeyValuePairs = true;
        }

        [Given(@"a fragment specifier, as follows: ""(.*)""")]
        public void GivenAFragmentSpecifierAsFollows(string fragment) {
            _fragment = fragment;
        }

        [Given(@"optimisation")]
        public void GivenOptimisationt() {
            _optimisation = true;
        }

        [Given(@"other query content: ""(.*)""")]
        public void GivenOtherQueryContent(string otherQueryContent) {
            _otherQueryContent = otherQueryContent;
        }

        [Given(@"the following Digital Link URI: ""(.*)""")]
        [Given(@"I want to convert the following Digital Link to an element string: ""(.*)""")]
        [Given(@"I want to convert the following compressed Digital Link to an element string: ""(.*)""")]
        public void GivenIWantToConvertADigitalLinkToElementStrings(string digitalLink) {
            _digitalLink = digitalLink;
        }

        [Given(@"I have an unbracketed GS1 element string ""(.*)""")]
        [Given(@"I want to create a compressed Digital Link for the GS1 element string ""(.*)""")]
        [Given(@"I want to create a Digital Link for the unbracketed GS1 element string ""(.*)""")]
        public void GivenIHaveAnUnbracketedGS1ElementString(string elementString) {
            _elementString = elementString
                .Replace("<gs>", "\u001d", StringComparison.InvariantCultureIgnoreCase);
        }

        [Given(@"I have a compressed Digital Link ""(.*)""")]
        [Given(@"I have a partially compressed Digital Link ""(.*)""")]
        public void GivenIHaveAnCompressedDigitalLink(string compressedDigitalLink) {
            _compressedDigitalLink = compressedDigitalLink;
        }

        [Given(@"I have a decompressed Digital Link ""(.*)""")]
        public void GivenIHaveADecompressedDigitalLink(string decompressedDigitalLink) {
            _decompressedDigitalLink = new(decompressedDigitalLink);
        }

        [When(@"I convert the Digital Link to an element string")]
        [When(@"I convert the compressed Digital Link to an element string")]
        public void WhenIConvertTheDigitalLinkToElementStrings() {
            _elementStringObj = DigitalLinkConvert.FromGs1DigitalLinkToGs1ElementString(_digitalLink);
        }

        [When(@"I convert the Digital Link to an element string with brackets")]
        [When(@"I convert the compressed Digital Link to an element string with brackets")]
        public void WhenIConvertTheDigitalLinkToElementStringsWithBrackets() {
            _elementStringObj = DigitalLinkConvert.FromGs1DigitalLinkToGs1ElementString(_digitalLink, true);
        }

        [When(@"I extract AIs and values")]
        public void WhenIExtractAIsAndValues() {
            try {
                _dataResult = DigitalLinkConvert.FromGs1ElementStringToData(_elementString);
            }
            catch (ArgumentException e) {
                _thrownException = e;
            }
        }

        [When(@"I extract AIs and values without validation")]
        public void WhenIExtractAIsAndValuesWithoutValidation() {
            _dataResult = DigitalLinkConvert.FromGs1ElementStringToData(_elementString, true);
        }

        [When(@"I extract AIs and values from the Digital Link")]
        [When(@"I extract AIs and values from the Digital Link with short names")]
        public void WhenIExtractAIsAndValuesFromTheDigitalLink() {
            _dataResult = _digitalLink?.FromGs1DigitalLinkToData();
        }

        [When(@"I extract data from the GS1 Digital Link")]
        public void WhenIExtractDataFromTheGs1DigitalLink() {
            try {
                _dataResult = DigitalLinkConvert.FromGs1DigitalLinkToData(_digitalLink ?? string.Empty);
            }
            catch (Exception e) {
                _thrownException = e;
            }
            _digitalLink = null;
        }

        [When(@"I decompress the compressed Digital Link")]
        public void WhenIDecompressTheCompressedDigitalLink() {
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevel(_compressedDigitalLink, CompressionLevel.Uncompressed);
        }

        [When(@"I decompress the compressed Digital Link to a Digital Link with short names")]
        public void WhenIDecompressTheCompressedDigitalLinkToADigitalLinkWithShortNames() {
#pragma warning disable CS0612 // Type or member is obsolete
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevelWithShortNames(_compressedDigitalLink, CompressionLevel.Uncompressed);
#pragma warning restore CS0612 // Type or member is obsolete
        }

        [When(@"I compress the decompressed Digital Link")]
        public void WhenICompressTheDecompressedDigitalLink() {
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevel(_decompressedDigitalLink.Value, CompressionLevel.Compressed);
        }

        [When(@"I compress the decompressed Digital Link with short names")]
        public void WhenICompressTheDecompressedDigitalLinkWithShortNames() {
#pragma warning disable CS0612 // Type or member is obsolete
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevelWithShortNames(_decompressedDigitalLink.Value, CompressionLevel.Compressed);
#pragma warning restore CS0612 // Type or member is obsolete
        }

        [When(@"I partially compress the decompressed Digital Link")]
        public void WhenIPartiallyCompressTheDecompressedDigitalLink() {
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevel(_decompressedDigitalLink.Value, CompressionLevel.PartiallyCompressed);
        }

        [When(@"I partially compress the decompressed Digital Link to a Digital Link with short names")]
        public void WhenIPartiallyCompressTheDecompressedDigitalLinkToADigitalLinkWithShortNames() {
#pragma warning disable CS0612 // Type or member is obsolete
            _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevelWithShortNames(_decompressedDigitalLink.Value, CompressionLevel.PartiallyCompressed);
#pragma warning restore CS0612 // Type or member is obsolete
        }

        [When(@"I build a GS1 Digital Link")]
        public void WhenIBuildAGs1DigitalLink() {

            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1DataToDigitalLink(
                _gs1Ais ?? [],
                null,
                _digitalLinkForm,
                _optimisation,
                nonGS1KeyValuePairs: _nonGs1KeyValuePairs,
                compressNonGs1KeyValuePairs: _compressNonGs1KeyValuePairs,
                otherQueryContent: _otherQueryContent,
                fragment: _fragment);
            }
            catch (Exception e) {
                _thrownException = e;
            }

            _compressNonGs1KeyValuePairs = false;
            _optimisation = false;
            _gs1Ais = null;
            _nonGs1KeyValuePairs = null;
        }

        [When(@"I translate the (?:.*\s)?element string to a Digital Link")]
        public void WhenIBuildAGs1DigitalLinkOverTheElementString() {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            bool isBracketed = stepText.Contains("bracketed element string");
            _inputColumnWidth = 66;
            _resultColumnWidth = 84;
            if (!isBracketed) {
                _elementString = _elementString?.Replace("<gs>", "\u001d");
            }

            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLink(
                _elementString ?? string.Empty,
                null,
                _digitalLinkForm,
                _optimisation,
                nonGS1KeyValuePairs: _nonGs1KeyValuePairs,
                compressNonGs1KeyValuePairs: _compressNonGs1KeyValuePairs,
                otherQueryContent: _otherQueryContent,
                fragment: _fragment);
            }
            catch (Exception e) {
                _thrownException = e;
            }

            _compressNonGs1KeyValuePairs = false;
            _optimisation = false;
            _elementString = null;
            _nonGs1KeyValuePairs = null;
        }

        [When(@"I translate the (?:.*\s)?element string to a Digital Link with a URI stem")]
        public void WhenITranslateTheElementStringToDigitalLinkWithAUriStem() {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            bool isBracketed = stepText.Contains("bracketed element string");
            _inputColumnWidth = 66;
            _resultColumnWidth = 84;
            if (!isBracketed) {
                _elementString = _elementString?.Replace("<gs>", "\u001d");
            }

            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLink(_elementString ?? string.Empty, uriStem: "dlr.trvst4hp.org");
            }
            catch (Exception e) {
                _thrownException = e;
            }
        }

        [When(@"I translate the (?:.*\s)?element string to a Digital Link with a URI stem set to (.*)")]
        public void WhenITranslateTheElementStringToDigitalLinkWithAUriStemSetTo(string uriStem) {
            _inputColumnWidth = 66;
            _resultColumnWidth = 84;
            _thrownException = null;
            _elementString = _elementString?.Replace("<gs>", "\u001d");

            uriStem = uriStem.Replace("<sp>", " ");
            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLink(_elementString, uriStem: uriStem);
            }
            catch (Exception e) {
                _thrownException = e;
            }
        }

        [When(@"I translate the element string to a compressed Digital Link")]
        public void WhenITranslateTheElementStringToCompressedDigitalLink() {
            _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLink(_elementString, digitalLinkForm: DigitalLinkForm.Compressed);
        }

        [When(@"I translate the element string to a partially compressed Digital Link")]
        public void WhenITranslateTheElementStringToPartiallyCompressedDigitalLink() {
            _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLink(_elementString, digitalLinkForm: DigitalLinkForm.PartiallyCompressed);
        }

        [When(@"I translate the element string to a partially compressed Digital Link that uses short text")]
        public void WhenITranslateTheElementStringToPartiallyCompressedDigitalLinkWithShortText() {
#pragma warning disable CS0618 // Type or member is obsolete
            _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLinkWithShortNames(_elementString, digitalLinkForm: DigitalLinkForm.PartiallyCompressed);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [When(@"I translate the (?:.*\s)?element string to a Digital Link with short text")]
        public void WhenITranslateTheElementStringToDigitalLinkWithShortText() {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            bool isBracketed = stepText.Contains("bracketed element string");
            _inputColumnWidth = 66;
            _resultColumnWidth = 84;
            if (!isBracketed) {
                _elementString = _elementString?.Replace("<gs>", "\u001d");
            }

            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLinkWithShortNames(_elementString ?? string.Empty);
            }
            catch (Exception e) {
                _thrownException = e;
            }
        }

        [When(@"I translate the (?:.*\s)?element string to a Digital Link with short text and a URI stem")]
        public void WhenITranslateTheElementStringToDigitalLinkWithShortNamesAndAUriStem() {
            _inputColumnWidth = 66;
            _resultColumnWidth = 84;
            _elementString = _elementString?.Replace("<gs>", "\u001d", StringComparison.InvariantCultureIgnoreCase);

#pragma warning disable CS0618 // Type or member is obsolete
            try {
                _digitalLinkObj = DigitalLinkConvert.FromGs1ElementStringToDigitalLinkWithShortNames(_elementString ?? string.Empty, uriStem: "dlr.trvst4hp.org");
            }
            catch (Exception e) {
                _thrownException = e;
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [When(@"I change the compression level of the GS1 Digital Link")]
        public void WhenIPartiallyCompressTheGs1DigitalLink() {
            try {
                _digitalLinkObj = DigitalLinkConvert.Gs1DigitalLinkCompressionLevel(
                _digitalLink ?? string.Empty,
                _compressionLevel,
                _optimisation,
                compressNonGs1KeyValuePairs: _compressNonGs1KeyValuePairs);
            }
            catch (Exception e) {
                _thrownException = e;
            }

            _compressNonGs1KeyValuePairs = false;
            _optimisation = false;
            _digitalLink = null;
            _nonGs1KeyValuePairs = null;
        }

        [When(@"I analyse the GS1 Digital Link URI")]
        public void WhenIAnalyseTheGs1DigitalLinkUri() {
            try {
                _analytics = new Gs1DigitalLink(_digitalLink ?? string.Empty).AnalyseUri(true);
            }
            catch (Exception e) {
                _thrownException = e;
            }
        }

        [When(@"I analyse the semantics of the GS1 Digital Link URI")]
        public void WhenIAnalyseTheSemanticsOfTheGs1DigitalLinkUri() {
            try {
                _semanticAnalytics = new Gs1DigitalLink(_digitalLink ?? string.Empty).AnalyseUriSemantics();
            }
            catch (Exception e) {
                _thrownException = e;
            }
        }


        [Then(@"the (?:compressed )?Digital Link should be ""(.*)""")]
        public void ThenTheCompressedVersionOfTheDigitalLinkShouldBe(string expectedDigitalLink) {
            LogFile.OutputLog = false;

            if (LogFile.OutputLog) {
                lock (lockObject) {
                    FileInfo fi = new("C:\\tmp\\output.txt");
                    using var sw = fi.AppendText();
                    if (_decompressedDigitalLink is not null) {
                        sw.WriteLine($"{new string(' ', 6)}| {_decompressedDigitalLink}{new string(' ', 76 - _decompressedDigitalLink.Value.Length)} | {_digitalLink}{new string(' ', 101 - (_digitalLinkObj?.Value.Length ?? 0))} |");
                    }
                    else {
                        sw.WriteLine($"{new string(' ', 6)}| {_elementString}{new string(' ', 66 - (_elementString?.Length ?? 0))} | {_digitalLinkObj}{new string(' ', 81 - (_digitalLinkObj?.Value.Length ?? 0))} |");
                    }
                    sw.Close();
                }
            }

            LogFile.OutputLog = false;

            Assert.NotNull(_digitalLinkObj);
            Assert.NotEmpty(_digitalLinkObj.Value);
            Assert.Equal(expectedDigitalLink, _digitalLinkObj.Value);

            _decompressedDigitalLink = null;
            _elementString = null;
        }

        [Then(@"I get a UriFormat exception")]
        public void ThenIShouldGetAUriFormatException() {
            Assert.IsType<UriFormatException>(_thrownException);
        }

        [Then(@"the element string should be ""(.*)""")]
        public void ThenTheDataExtractedFromTheDigitalLinkShouldContain(string expectedElementStrings) {
            LogFile.OutputLog = false;

            if (LogFile.OutputLog) {
                lock (lockObject) {
                    FileInfo fi = new FileInfo("C:\\tmp\\output.txt");
                    using var sw = fi.AppendText();
                    sw.WriteLine($"{new string(' ', 6)}| {_digitalLink}{new string(' ', _inputColumnWidth - _digitalLink.Length)} | {_elementStringObj}{new string(' ', _resultColumnWidth - _elementStringObj.Value.Length)} |");
                    sw.Close();
                }
            }
            LogFile.OutputLog = false;

            Assert.NotNull(_elementStringObj?.Value);
            Assert.Equal(expectedElementStrings.Replace("<GS>", "\u001D", StringComparison.InvariantCultureIgnoreCase), _elementStringObj.Value);
        }

        [Then(@"the data should contain:")]
        public void ThenTheResultShouldContain(Table table) {
            Assert.NotNull(_digitalLinkObj);

            foreach (var row in table.Rows) {
                var ai = row["AI"];
                var expectedValue = row["Value"];
                Assert.True(_dataResult?.Gs1AIs.ContainsKey(ai), $"Result should contain AI {ai}");
                Assert.Equal(expectedValue, _dataResult?.Gs1AIs[ai]);
            }
        }

        [Then(@"an exception with message ""(.*)"" is thrown")]
        public void ThenAnExceptionWithMessageIsThrown(string expectedMessage) {
            Assert.NotNull(_thrownException);
            Assert.Contains(expectedMessage.Replace("\\r", "\r").Replace("\\n", "\n"), _thrownException.Message);
        }

        [Then(@"the data extracted from the Digital Link should contain:")]
        public void ThenTheDataExtractedFromTheDigitalLinkShouldContain(Table table) {
            Assert.NotNull(_digitalLinkObj);

            foreach (var row in table.Rows) {
                var ai = row["AI"];
                if (string.IsNullOrWhiteSpace(ai)) {
                    continue;
                }
                var expectedValue = row["Value"];
                var gs1Kvps = _dataResult?.Gs1AIs;
                Assert.True(gs1Kvps?.ContainsKey(ai), $"Result should contain AI {ai}");
                Assert.Equal(expectedValue, gs1Kvps?[ai]);
            }
        }

        [Then(@"the decompressed Digital Link should be ""(.*)""")]
        public void ThenTheDataExtractedFromTheCompressedDigitalLinkShouldBe(string expectedDigitalLink) {
            LogFile.OutputLog = false;

            if (LogFile.OutputLog) {
                lock (lockObject) {
                    FileInfo fi = new FileInfo("C:\\tmp\\output.txt");
                    using var sw = fi.AppendText();
                    sw.WriteLine($"{new string(' ', 6)}| {_compressedDigitalLink}{new string(' ', 76 - _compressedDigitalLink.Length)} | {_digitalLink}{new string(' ', 101 - (_digitalLinkObj?.Value.Length ?? 0))} |");
                    sw.Close();
                }
            }

            LogFile.OutputLog = false;

            Assert.NotNull(_digitalLinkObj);
            Assert.NotEmpty(_digitalLinkObj.Value);
            Assert.Equal(expectedDigitalLink, _digitalLinkObj.Value);
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string expectedResult) {
            Assert.Equal(expectedResult, _digitalLinkObj?.Value);
        }

        [Then(@"the data result should be:")]
        public void ThenTheDataResultShouldBe(Table table) {
            // Create an instance of Gs1Data and populate its properties
            var expectedGs1Data = new Gs1Data();
            Dictionary<string, string> gs1Ais = [];
            Dictionary<string, string> nonGs1KeyValuePairs = [];
            var otherQueryStringContent = string.Empty;
            var fgragmentSpecifier = string.Empty;

            foreach (var row in table.Rows) {
                var propertyName = row["PropertyName"];
                var value = row["Value"];

                switch (propertyName) {
                    case "Gs1AIs":
                        gs1Ais = JsonConvert.DeserializeObject<Dictionary<string, string>>(value) ?? [];
                        break;
                    case "NonGs1KeyValuePairs":
                        nonGs1KeyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(value) ?? [];
                        break;
                    case "OtherQueryStringContent":
                        otherQueryStringContent = value;
                        break;
                    case "FragmentSpecifier":
                        fgragmentSpecifier = value;
                        break;
                    default:
                        throw new ArgumentException($"Unknown property: {propertyName}");
                }
            }

            expectedGs1Data = new Gs1Data(gs1Ais ?? [], nonGs1KeyValuePairs, otherQueryStringContent, fgragmentSpecifier);

            void testGs1AiKvp(KeyValuePair<string, string> e) => Assert.True(e.Value == _dataResult.Gs1AIs[e.Key]);
            void testNonGs1Kvp(KeyValuePair<string, string> e) => Assert.True(e.Value == _dataResult.NonGs1KeyValuePairs[e.Key]);

#pragma warning disable IDE0039 // Use local function
            Action<KeyValuePair<string, string>> valueInspectorGs1 = kvp => testGs1AiKvp(kvp);
            Action<KeyValuePair<string, string>> valueInspectorNonGs1 = kvp => testNonGs1Kvp(kvp);
#pragma warning restore IDE0039 // Use local function

            var gs1AIsValueInspectors = (
                from _ in gs1Ais ?? []
                select valueInspectorGs1).ToList();

            var nonGs1KeyValuePairsInspectors = (
                from _ in nonGs1KeyValuePairs
                select valueInspectorNonGs1).ToList();

            // Compare the expected and actual Gs1Data objects
            Assert.Collection(expectedGs1Data.Gs1AIs, [.. gs1AIsValueInspectors]);
            Assert.Collection(expectedGs1Data.NonGs1KeyValuePairs, [.. nonGs1KeyValuePairsInspectors]);
            Assert.Equal(expectedGs1Data.OtherQueryStringContent, _dataResult.OtherQueryStringContent);
            Assert.Equal(expectedGs1Data.FragmentSpecifier, _dataResult.FragmentSpecifier);
        }

        [Then(@"the analysis should contain:")]
        public void ThenTheAnalysisShouldContainBe(Table table) {
            // Create an instance of Gs1Data and populate its properties
            string expectedDetectedForm = string.Empty;
            string expectedElementStringOutput = string.Empty;
            var expectedPathComponents = string.Empty;
            var expectedQueryString = string.Empty;
            var expectedUriStem = string.Empty;

            foreach (var row in table.Rows) {
                var propertyName = row["PropertyName"];
                var value = row["Value"];

                switch (propertyName) {
                    case "DetectedForm":
                        expectedDetectedForm = value;
                        break;
                    case "ElementStringOutput":
                        expectedElementStringOutput = value;
                        break;
                    case "PathComponents":
                        expectedPathComponents = value;
                        break;
                    case "QueryString":
                        expectedQueryString = value;
                        break;
                    case "UriStem":
                        expectedUriStem = value;
                        break;
                    default:
                        throw new ArgumentException($"Unknown property: {propertyName}");
                }
            }

            // Compare the expected and actual Gs1Data objects
            Assert.Equal(expectedDetectedForm, _analytics.DetectedForm.ToString());
            Assert.Equal(expectedElementStringOutput, _analytics.ElementStringOutput);
            Assert.Equal(expectedPathComponents, _analytics.PathComponents);
            Assert.Equal(expectedQueryString, _analytics.QueryString);
            Assert.Equal(expectedUriStem, _analytics.UriStem);
        }

        [Then(@"the semantic analysis should contain:")]
        public void ThenTheSemanticAnalysisShouldContainBe(Table table) {
            // Create an instance of Gs1Data and populate its properties
            string expectedGs1Gtin = string.Empty;
            string expectedSchemaGtin = string.Empty;
            var expectedGs1HasBatchLot = string.Empty;
            var expectedGs1ConsumerProductVariant = string.Empty;
            var expectedGs1ExpirationDate = string.Empty;
            var expectedGs1ElementStrings = string.Empty;

            foreach (var row in table.Rows) {
                var propertyName = row["PropertyName"];
                var value = row["Value"];

                switch (propertyName) {
                    case "gs1:gtin":
                        expectedGs1Gtin = value;
                        break;
                    case "schema:gtin":
                        expectedSchemaGtin = value;
                        break;
                    case "gs1:hasBatchLot":
                        expectedGs1HasBatchLot = value;
                        break;
                    case "gs1:consumerProductVariant":
                        expectedGs1ConsumerProductVariant = value;
                        break;
                    case "gs1:expirationDate":
                        expectedGs1ExpirationDate = value;
                        break;
                    case "gs1:elementStrings":
                        expectedGs1ElementStrings = value;
                        break;
                    default:
                        throw new ArgumentException($"Unknown property: {propertyName}");
                }
            }

            Assert.Equal(expectedGs1Gtin, _semanticAnalytics["gs1:gtin"].ToString());
            Assert.Equal(expectedSchemaGtin, _semanticAnalytics["schema:gtin"].ToString());
            Assert.Equal(expectedGs1HasBatchLot, _semanticAnalytics["gs1:hasBatchLot"].ToString());
            Assert.Equal(expectedGs1ConsumerProductVariant, _semanticAnalytics["gs1:consumerProductVariant"].ToString());
            Assert.Equal(expectedGs1ExpirationDate, _semanticAnalytics["gs1:expirationDate"].ToString());
            Assert.Equal(expectedGs1ElementStrings, _semanticAnalytics["gs1:elementStrings"].ToString());
        }
    }
}

