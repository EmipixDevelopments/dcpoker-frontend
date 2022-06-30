using System;
using System.Collections.Generic;

[Serializable]
public class PhoneCodeAndFlagListData
{
    public List<CodeAndFlag> List = new List<CodeAndFlag>();

    public void InitializeUsingSettings()
    {
        // in code
        //List.Add(new CodeAndFlag() { PhoneCode = "+1", FlagName = "united-states" });//United States
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-264", FlagName = "anguilla" });//Anguilla
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-284", FlagName = "british-virgin-islands" });//British Virgin Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-340", FlagName = "us-virgin-islands" });//U.S. Virgin Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-345", FlagName = "cayman-islands" });//Cayman Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-441", FlagName = "bermuda" });//Bermuda
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-649", FlagName = "turks-caicos-islands" });//Turks and Caicos Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-664", FlagName = "montserrat" });//Montserrat
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-670", FlagName = "northern-mariana-islands" });//Northern Mariana Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-671", FlagName = "guam" });//Guam
        //List.Add(new CodeAndFlag() { PhoneCode = "+1-684", FlagName = "american-samoa" });//American Samoa
        //List.Add(new CodeAndFlag() { PhoneCode = "+244", FlagName = "angola" });//Angola
        //List.Add(new CodeAndFlag() { PhoneCode = "+246", FlagName = "british-indian-ocean-territory" });//British Indian Ocean Territory
        //List.Add(new CodeAndFlag() { PhoneCode = "+290", FlagName = "st-helena" });//Saint Helena
        //List.Add(new CodeAndFlag() { PhoneCode = "+298", FlagName = "faroe-islands" });//Faroe Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+32", FlagName = "belgium" });//Belgium
        //List.Add(new CodeAndFlag() { PhoneCode = "+350", FlagName = "gibraltar" });//Gibraltar
        //List.Add(new CodeAndFlag() { PhoneCode = "+357", FlagName = "cyprus" });//Cyprus
        //List.Add(new CodeAndFlag() { PhoneCode = "+359", FlagName = "bulgaria" });//Bulgaria
        //List.Add(new CodeAndFlag() { PhoneCode = "+372", FlagName = "estonia" });//Estonia
        //List.Add(new CodeAndFlag() { PhoneCode = "+385", FlagName = "croatia" });//Croatia
        //List.Add(new CodeAndFlag() { PhoneCode = "+39", FlagName = "italy" });//Italy
        //List.Add(new CodeAndFlag() { PhoneCode = "+40", FlagName = "romania" });//Romania
        //List.Add(new CodeAndFlag() { PhoneCode = "+43", FlagName = "austria" });//Austria
        //List.Add(new CodeAndFlag() { PhoneCode = "+44", FlagName = "united-kingdom" });//United Kingdom
        //List.Add(new CodeAndFlag() { PhoneCode = "+44-1481", FlagName = "guernsey" });//Guernsey
        //List.Add(new CodeAndFlag() { PhoneCode = "+44-1534", FlagName = "jersey" });//Jersey
        //List.Add(new CodeAndFlag() { PhoneCode = "+44-1624", FlagName = "isle-of-man" });//Isle of Man
        //List.Add(new CodeAndFlag() { PhoneCode = "+48", FlagName = "poland" });//Poland
        //List.Add(new CodeAndFlag() { PhoneCode = "+49", FlagName = "germany" });//Germany
        //List.Add(new CodeAndFlag() { PhoneCode = "+500", FlagName = "falkland-islands" });//Falkland Islands
        //List.Add(new CodeAndFlag() { PhoneCode = "+507", FlagName = "panama" });//Panama
        //List.Add(new CodeAndFlag() { PhoneCode = "+599", FlagName = "netherlands-antilles" });//Netherlands Antilles
        //List.Add(new CodeAndFlag() { PhoneCode = "+64", FlagName = "pitcairn-islands" });//Pitcairn
        //List.Add(new CodeAndFlag() { PhoneCode = "+65", FlagName = "singapore" });//Singapore
        //List.Add(new CodeAndFlag() { PhoneCode = "+672", FlagName = "antarctica" });//Antarctica
        //List.Add(new CodeAndFlag() { PhoneCode = "+673", FlagName = "brunei" });//Brunei
        //List.Add(new CodeAndFlag() { PhoneCode = "+689", FlagName = "french-polynesia" });//French Polynesia
        //List.Add(new CodeAndFlag() { PhoneCode = "+81", FlagName = "japan" });//Japan
        //List.Add(new CodeAndFlag() { PhoneCode = "+850", FlagName = "north-korea" });//North Korea
        //List.Add(new CodeAndFlag() { PhoneCode = "+855", FlagName = "cambodia" });//Cambodia
        //List.Add(new CodeAndFlag() { PhoneCode = "+94", FlagName = "sri-lanka" });//Sri Lanka
        //List.Add(new CodeAndFlag() { PhoneCode = "+95", FlagName = "myanmar-burma" });//Myanmar
        //List.Add(new CodeAndFlag() { PhoneCode = "+968", FlagName = "oman" });//Oman
        //List.Add(new CodeAndFlag() { PhoneCode = "+970", FlagName = "palestinian-territories" });//Palestine
        //List.Add(new CodeAndFlag() { PhoneCode = "+974", FlagName = "qatar" });//Qatar
        //List.Add(new CodeAndFlag() { PhoneCode = "+995", FlagName = "georgia" });//Georgia
        List.Add(new CodeAndFlag() { PhoneCode = "+1", FlagName = "canada" });//Canada
        List.Add(new CodeAndFlag() { PhoneCode = "+1-242", FlagName = "bahamas" });//Bahamas
        List.Add(new CodeAndFlag() { PhoneCode = "+1-246", FlagName = "barbados" });//Barbados
        List.Add(new CodeAndFlag() { PhoneCode = "+1-268", FlagName = "antigua-barbuda" });//Antigua and Barbuda
        List.Add(new CodeAndFlag() { PhoneCode = "+1-473", FlagName = "grenada" });//Grenada
        List.Add(new CodeAndFlag() { PhoneCode = "+1-721", FlagName = "sint-maarten" });//Sint Maarten
        List.Add(new CodeAndFlag() { PhoneCode = "+1-758", FlagName = "st-lucia" });//Saint Lucia
        List.Add(new CodeAndFlag() { PhoneCode = "+1-767", FlagName = "dominica" });//Dominica
        List.Add(new CodeAndFlag() { PhoneCode = "+1-784", FlagName = "st-vincent-grenadines" });//Saint Vincent and the Grenadines
        List.Add(new CodeAndFlag() { PhoneCode = "+1-787", FlagName = "puerto-rico" });//Puerto Rico
        List.Add(new CodeAndFlag() { PhoneCode = "+1-809", FlagName = "dominican-republic" });//Dominican Republic
        List.Add(new CodeAndFlag() { PhoneCode = "+1-829", FlagName = "dominican-republic" });//Dominican Republic
        List.Add(new CodeAndFlag() { PhoneCode = "+1-849", FlagName = "dominican-republic" });//Dominican Republic
        List.Add(new CodeAndFlag() { PhoneCode = "+1-868", FlagName = "trinidad-tobago" });//Trinidad and Tobago
        List.Add(new CodeAndFlag() { PhoneCode = "+1-869", FlagName = "st-kitts-nevis" });//Saint Kitts and Nevis
        List.Add(new CodeAndFlag() { PhoneCode = "+1-876", FlagName = "jamaica" });//Jamaica
        List.Add(new CodeAndFlag() { PhoneCode = "+1-939", FlagName = "puerto-rico" });//Puerto Rico
        List.Add(new CodeAndFlag() { PhoneCode = "+20", FlagName = "egypt" });//Egypt
        List.Add(new CodeAndFlag() { PhoneCode = "+211", FlagName = "south-sudan" });//South Sudan
        List.Add(new CodeAndFlag() { PhoneCode = "+212", FlagName = "morocco" });//Morocco
        List.Add(new CodeAndFlag() { PhoneCode = "+212", FlagName = "western-sahara" });//Western Sahara
        List.Add(new CodeAndFlag() { PhoneCode = "+213", FlagName = "algeria" });//Algeria
        List.Add(new CodeAndFlag() { PhoneCode = "+216", FlagName = "tunisia" });//Tunisia
        List.Add(new CodeAndFlag() { PhoneCode = "+218", FlagName = "libya" });//Libya
        List.Add(new CodeAndFlag() { PhoneCode = "+220", FlagName = "gambia" });//Gambia
        List.Add(new CodeAndFlag() { PhoneCode = "+221", FlagName = "senegal" });//Senegal
        List.Add(new CodeAndFlag() { PhoneCode = "+222", FlagName = "mauritania" });//Mauritania
        List.Add(new CodeAndFlag() { PhoneCode = "+223", FlagName = "mali" });//Mali
        List.Add(new CodeAndFlag() { PhoneCode = "+224", FlagName = "guinea" });//Guinea
        List.Add(new CodeAndFlag() { PhoneCode = "+225", FlagName = "ivory-coast" });//Ivory Coast
        List.Add(new CodeAndFlag() { PhoneCode = "+226", FlagName = "burkina-faso" });//Burkina Faso
        List.Add(new CodeAndFlag() { PhoneCode = "+227", FlagName = "niger" });//Niger
        List.Add(new CodeAndFlag() { PhoneCode = "+228", FlagName = "togo" });//Togo
        List.Add(new CodeAndFlag() { PhoneCode = "+229", FlagName = "benin" });//Benin
        List.Add(new CodeAndFlag() { PhoneCode = "+230", FlagName = "mauritius" });//Mauritius
        List.Add(new CodeAndFlag() { PhoneCode = "+231", FlagName = "liberia" });//Liberia
        List.Add(new CodeAndFlag() { PhoneCode = "+232", FlagName = "sierra-leone" });//Sierra Leone
        List.Add(new CodeAndFlag() { PhoneCode = "+233", FlagName = "ghana" });//Ghana
        List.Add(new CodeAndFlag() { PhoneCode = "+234", FlagName = "nigeria" });//Nigeria
        List.Add(new CodeAndFlag() { PhoneCode = "+235", FlagName = "chad" });//Chad
        List.Add(new CodeAndFlag() { PhoneCode = "+236", FlagName = "central-african-republic" });//Central African Republic
        List.Add(new CodeAndFlag() { PhoneCode = "+237", FlagName = "cameroon" });//Cameroon
        List.Add(new CodeAndFlag() { PhoneCode = "+238", FlagName = "cape-verde" });//Cape Verde
        List.Add(new CodeAndFlag() { PhoneCode = "+239", FlagName = "sao-tome-principe" });//Sao Tome and Principe
        List.Add(new CodeAndFlag() { PhoneCode = "+240", FlagName = "equatorial-guinea" });//Equatorial Guinea
        List.Add(new CodeAndFlag() { PhoneCode = "+241", FlagName = "gabon" });//Gabon
        List.Add(new CodeAndFlag() { PhoneCode = "+242", FlagName = "congo-brazzaville" });//Republic of the Congo
        List.Add(new CodeAndFlag() { PhoneCode = "+243", FlagName = "congo-kinshasa" });//Democratic Republic of the Congo
        List.Add(new CodeAndFlag() { PhoneCode = "+245", FlagName = "guinea-bissau" });//Guinea-Bissau
        List.Add(new CodeAndFlag() { PhoneCode = "+248", FlagName = "seychelles" });//Seychelles
        List.Add(new CodeAndFlag() { PhoneCode = "+249", FlagName = "sudan" });//Sudan
        List.Add(new CodeAndFlag() { PhoneCode = "+250", FlagName = "rwanda" });//Rwanda
        List.Add(new CodeAndFlag() { PhoneCode = "+251", FlagName = "ethiopia" });//Ethiopia
        List.Add(new CodeAndFlag() { PhoneCode = "+252", FlagName = "somalia" });//Somalia
        List.Add(new CodeAndFlag() { PhoneCode = "+253", FlagName = "djibouti" });//Djibouti
        List.Add(new CodeAndFlag() { PhoneCode = "+254", FlagName = "kenya" });//Kenya
        List.Add(new CodeAndFlag() { PhoneCode = "+255", FlagName = "tanzania" });//Tanzania
        List.Add(new CodeAndFlag() { PhoneCode = "+256", FlagName = "uganda" });//Uganda
        List.Add(new CodeAndFlag() { PhoneCode = "+257", FlagName = "burundi" });//Burundi
        List.Add(new CodeAndFlag() { PhoneCode = "+258", FlagName = "mozambique" });//Mozambique
        List.Add(new CodeAndFlag() { PhoneCode = "+260", FlagName = "zambia" });//Zambia
        List.Add(new CodeAndFlag() { PhoneCode = "+261", FlagName = "madagascar" });//Madagascar
        List.Add(new CodeAndFlag() { PhoneCode = "+262", FlagName = "mayotte" });//Mayotte
        List.Add(new CodeAndFlag() { PhoneCode = "+262", FlagName = "reunion" });//Reunion
        List.Add(new CodeAndFlag() { PhoneCode = "+263", FlagName = "zimbabwe" });//Zimbabwe
        List.Add(new CodeAndFlag() { PhoneCode = "+264", FlagName = "namibia" });//Namibia
        List.Add(new CodeAndFlag() { PhoneCode = "+265", FlagName = "malawi" });//Malawi
        List.Add(new CodeAndFlag() { PhoneCode = "+266", FlagName = "lesotho" });//Lesotho
        List.Add(new CodeAndFlag() { PhoneCode = "+267", FlagName = "botswana" });//Botswana
        List.Add(new CodeAndFlag() { PhoneCode = "+268", FlagName = "swaziland" });//Swaziland
        List.Add(new CodeAndFlag() { PhoneCode = "+269", FlagName = "comoros" });//Comoros
        List.Add(new CodeAndFlag() { PhoneCode = "+27", FlagName = "south-africa" });//South Africa
        List.Add(new CodeAndFlag() { PhoneCode = "+291", FlagName = "eritrea" });//Eritrea
        List.Add(new CodeAndFlag() { PhoneCode = "+297", FlagName = "aruba" });//Aruba
        List.Add(new CodeAndFlag() { PhoneCode = "+299", FlagName = "greenland" });//Greenland
        List.Add(new CodeAndFlag() { PhoneCode = "+30", FlagName = "greece" });//Greece
        List.Add(new CodeAndFlag() { PhoneCode = "+31", FlagName = "netherlands" });//Netherlands
        List.Add(new CodeAndFlag() { PhoneCode = "+33", FlagName = "france" });//France
        List.Add(new CodeAndFlag() { PhoneCode = "+34", FlagName = "spain" });//Spain
        List.Add(new CodeAndFlag() { PhoneCode = "+351", FlagName = "portugal" });//Portugal
        List.Add(new CodeAndFlag() { PhoneCode = "+352", FlagName = "luxembourg" });//Luxembourg
        List.Add(new CodeAndFlag() { PhoneCode = "+353", FlagName = "ireland" });//Ireland
        List.Add(new CodeAndFlag() { PhoneCode = "+354", FlagName = "iceland" });//Iceland
        List.Add(new CodeAndFlag() { PhoneCode = "+355", FlagName = "albania" });//Albania
        List.Add(new CodeAndFlag() { PhoneCode = "+356", FlagName = "malta" });//Malta
        List.Add(new CodeAndFlag() { PhoneCode = "+358", FlagName = "finland" });//Finland
        List.Add(new CodeAndFlag() { PhoneCode = "+36", FlagName = "hungary" });//Hungary
        List.Add(new CodeAndFlag() { PhoneCode = "+370", FlagName = "lithuania" });//Lithuania
        List.Add(new CodeAndFlag() { PhoneCode = "+371", FlagName = "latvia" });//Latvia
        List.Add(new CodeAndFlag() { PhoneCode = "+373", FlagName = "moldova" });//Moldova
        List.Add(new CodeAndFlag() { PhoneCode = "+374", FlagName = "armenia" });//Armenia
        List.Add(new CodeAndFlag() { PhoneCode = "+375", FlagName = "belarus" });//Belarus
        List.Add(new CodeAndFlag() { PhoneCode = "+376", FlagName = "andorra" });//Andorra
        List.Add(new CodeAndFlag() { PhoneCode = "+377", FlagName = "monaco" });//Monaco
        List.Add(new CodeAndFlag() { PhoneCode = "+378", FlagName = "san-marino" });//San Marino
        List.Add(new CodeAndFlag() { PhoneCode = "+379", FlagName = "vatican-city" });//Vatican
        List.Add(new CodeAndFlag() { PhoneCode = "+380", FlagName = "ukraine" });//Ukraine
        List.Add(new CodeAndFlag() { PhoneCode = "+381", FlagName = "serbia" });//Serbia
        List.Add(new CodeAndFlag() { PhoneCode = "+382", FlagName = "montenegro" });//Montenegro
        List.Add(new CodeAndFlag() { PhoneCode = "+383", FlagName = "kosovo" });//Kosovo
        List.Add(new CodeAndFlag() { PhoneCode = "+386", FlagName = "slovenia" });//Slovenia
        List.Add(new CodeAndFlag() { PhoneCode = "+387", FlagName = "bosnia-herzegovina" });//Bosnia and Herzegovina
        List.Add(new CodeAndFlag() { PhoneCode = "+389", FlagName = "north-macedonia" });//Macedonia
        List.Add(new CodeAndFlag() { PhoneCode = "+41", FlagName = "switzerland" });//Switzerland
        List.Add(new CodeAndFlag() { PhoneCode = "+420", FlagName = "czechia" });//Czech Republic
        List.Add(new CodeAndFlag() { PhoneCode = "+421", FlagName = "slovakia" });//Slovakia
        List.Add(new CodeAndFlag() { PhoneCode = "+423", FlagName = "liechtenstein" });//Liechtenstein
        List.Add(new CodeAndFlag() { PhoneCode = "+45", FlagName = "denmark" });//Denmark
        List.Add(new CodeAndFlag() { PhoneCode = "+46", FlagName = "sweden" });//Sweden
        List.Add(new CodeAndFlag() { PhoneCode = "+47", FlagName = "norway" });//Norway
        List.Add(new CodeAndFlag() { PhoneCode = "+47", FlagName = "svalbard-jan-mayen" });//Svalbard and Jan Mayen
        List.Add(new CodeAndFlag() { PhoneCode = "+501", FlagName = "belize" });//Belize
        List.Add(new CodeAndFlag() { PhoneCode = "+502", FlagName = "guatemala" });//Guatemala
        List.Add(new CodeAndFlag() { PhoneCode = "+503", FlagName = "el-salvador" });//El Salvador
        List.Add(new CodeAndFlag() { PhoneCode = "+504", FlagName = "honduras" });//Honduras
        List.Add(new CodeAndFlag() { PhoneCode = "+505", FlagName = "nicaragua" });//Nicaragua
        List.Add(new CodeAndFlag() { PhoneCode = "+506", FlagName = "costa-rica" });//Costa Rica
        List.Add(new CodeAndFlag() { PhoneCode = "+508", FlagName = "st-pierre-miquelon" });//Saint Pierre and Miquelon
        List.Add(new CodeAndFlag() { PhoneCode = "+509", FlagName = "haiti" });//Haiti
        List.Add(new CodeAndFlag() { PhoneCode = "+51", FlagName = "peru" });//Peru
        List.Add(new CodeAndFlag() { PhoneCode = "+52", FlagName = "mexico" });//Mexico
        List.Add(new CodeAndFlag() { PhoneCode = "+53", FlagName = "cuba" });//Cuba
        List.Add(new CodeAndFlag() { PhoneCode = "+54", FlagName = "argentina" });//Argentina
        List.Add(new CodeAndFlag() { PhoneCode = "+55", FlagName = "brazil" });//Brazil
        List.Add(new CodeAndFlag() { PhoneCode = "+56", FlagName = "chile" });//Chile
        List.Add(new CodeAndFlag() { PhoneCode = "+57", FlagName = "colombia" });//Colombia
        List.Add(new CodeAndFlag() { PhoneCode = "+58", FlagName = "venezuela" });//Venezuela
        List.Add(new CodeAndFlag() { PhoneCode = "+590", FlagName = "st-barthelemy" });//Saint Barthelemy
        List.Add(new CodeAndFlag() { PhoneCode = "+590", FlagName = "st-martin" });//Saint Martin
        List.Add(new CodeAndFlag() { PhoneCode = "+591", FlagName = "bolivia" });//Bolivia
        List.Add(new CodeAndFlag() { PhoneCode = "+592", FlagName = "guyana" });//Guyana
        List.Add(new CodeAndFlag() { PhoneCode = "+593", FlagName = "ecuador" });//Ecuador
        List.Add(new CodeAndFlag() { PhoneCode = "+595", FlagName = "paraguay" });//Paraguay
        List.Add(new CodeAndFlag() { PhoneCode = "+597", FlagName = "suriname" });//Suriname
        List.Add(new CodeAndFlag() { PhoneCode = "+598", FlagName = "uruguay" });//Uruguay
        List.Add(new CodeAndFlag() { PhoneCode = "+599", FlagName = "curacao" });//Curacao
        List.Add(new CodeAndFlag() { PhoneCode = "+60", FlagName = "malaysia" });//Malaysia
        List.Add(new CodeAndFlag() { PhoneCode = "+61", FlagName = "australia" });//Australia
        List.Add(new CodeAndFlag() { PhoneCode = "+61", FlagName = "christmas-island" });//Christmas Island
        List.Add(new CodeAndFlag() { PhoneCode = "+61", FlagName = "cocos-keeling-islands" });//Cocos Islands
        List.Add(new CodeAndFlag() { PhoneCode = "+62", FlagName = "indonesia" });//Indonesia
        List.Add(new CodeAndFlag() { PhoneCode = "+63", FlagName = "philippines" });//Philippines
        List.Add(new CodeAndFlag() { PhoneCode = "+64", FlagName = "new-zealand" });//New Zealand
        List.Add(new CodeAndFlag() { PhoneCode = "+66", FlagName = "thailand" });//Thailand
        List.Add(new CodeAndFlag() { PhoneCode = "+670", FlagName = "timor-leste" });//East Timor
        List.Add(new CodeAndFlag() { PhoneCode = "+674", FlagName = "nauru" });//Nauru
        List.Add(new CodeAndFlag() { PhoneCode = "+675", FlagName = "papua-new-guinea" });//Papua New Guinea
        List.Add(new CodeAndFlag() { PhoneCode = "+676", FlagName = "tonga" });//Tonga
        List.Add(new CodeAndFlag() { PhoneCode = "+677", FlagName = "solomon-islands" });//Solomon Islands
        List.Add(new CodeAndFlag() { PhoneCode = "+678", FlagName = "vanuatu" });//Vanuatu
        List.Add(new CodeAndFlag() { PhoneCode = "+679", FlagName = "fiji" });//Fiji
        List.Add(new CodeAndFlag() { PhoneCode = "+680", FlagName = "palau" });//Palau
        List.Add(new CodeAndFlag() { PhoneCode = "+681", FlagName = "wallis-futuna" });//Wallis and Futuna
        List.Add(new CodeAndFlag() { PhoneCode = "+682", FlagName = "cook-islands" });//Cook Islands
        List.Add(new CodeAndFlag() { PhoneCode = "+683", FlagName = "niue" });//Niue
        List.Add(new CodeAndFlag() { PhoneCode = "+685", FlagName = "samoa" });//Samoa
        List.Add(new CodeAndFlag() { PhoneCode = "+686", FlagName = "kiribati" });//Kiribati
        List.Add(new CodeAndFlag() { PhoneCode = "+687", FlagName = "new-caledonia" });//New Caledonia
        List.Add(new CodeAndFlag() { PhoneCode = "+688", FlagName = "tuvalu" });//Tuvalu
        List.Add(new CodeAndFlag() { PhoneCode = "+690", FlagName = "tokelau" });//Tokelau
        List.Add(new CodeAndFlag() { PhoneCode = "+691", FlagName = "micronesia" });//Micronesia
        List.Add(new CodeAndFlag() { PhoneCode = "+692", FlagName = "marshall-islands" });//Marshall Islands
        List.Add(new CodeAndFlag() { PhoneCode = "+7", FlagName = "kazakhstan" });//Kazakhstan
        List.Add(new CodeAndFlag() { PhoneCode = "+7", FlagName = "russia" });//Russia
        List.Add(new CodeAndFlag() { PhoneCode = "+82", FlagName = "south-korea" });//South Korea
        List.Add(new CodeAndFlag() { PhoneCode = "+84", FlagName = "vietnam" });//Vietnam
        List.Add(new CodeAndFlag() { PhoneCode = "+852", FlagName = "hong-kong-sar-china" });//Hong Kong
        List.Add(new CodeAndFlag() { PhoneCode = "+853", FlagName = "macao-sar-china" });//Macau
        List.Add(new CodeAndFlag() { PhoneCode = "+856", FlagName = "laos" });//Laos
        List.Add(new CodeAndFlag() { PhoneCode = "+86", FlagName = "china" });//China
        List.Add(new CodeAndFlag() { PhoneCode = "+880", FlagName = "bangladesh" });//Bangladesh
        List.Add(new CodeAndFlag() { PhoneCode = "+886", FlagName = "taiwan" });//Taiwan
        List.Add(new CodeAndFlag() { PhoneCode = "+90", FlagName = "turkey" });//Turkey
        List.Add(new CodeAndFlag() { PhoneCode = "+91", FlagName = "india" });//India
        List.Add(new CodeAndFlag() { PhoneCode = "+92", FlagName = "pakistan" });//Pakistan
        List.Add(new CodeAndFlag() { PhoneCode = "+93", FlagName = "afghanistan" });//Afghanistan
        List.Add(new CodeAndFlag() { PhoneCode = "+960", FlagName = "maldives" });//Maldives
        List.Add(new CodeAndFlag() { PhoneCode = "+961", FlagName = "lebanon" });//Lebanon
        List.Add(new CodeAndFlag() { PhoneCode = "+962", FlagName = "jordan" });//Jordan
        List.Add(new CodeAndFlag() { PhoneCode = "+963", FlagName = "syria" });//Syria
        List.Add(new CodeAndFlag() { PhoneCode = "+964", FlagName = "iraq" });//Iraq
        List.Add(new CodeAndFlag() { PhoneCode = "+965", FlagName = "kuwait" });//Kuwait
        List.Add(new CodeAndFlag() { PhoneCode = "+966", FlagName = "saudi-arabia" });//Saudi Arabia
        List.Add(new CodeAndFlag() { PhoneCode = "+967", FlagName = "yemen" });//Yemen
        List.Add(new CodeAndFlag() { PhoneCode = "+971", FlagName = "united-arab-emirates" });//United Arab Emirates
        List.Add(new CodeAndFlag() { PhoneCode = "+972", FlagName = "israel" });//Israel
        List.Add(new CodeAndFlag() { PhoneCode = "+973", FlagName = "bahrain" });//Bahrain
        List.Add(new CodeAndFlag() { PhoneCode = "+975", FlagName = "bhutan" });//Bhutan
        List.Add(new CodeAndFlag() { PhoneCode = "+976", FlagName = "mongolia" });//Mongolia
        List.Add(new CodeAndFlag() { PhoneCode = "+977", FlagName = "nepal" });//Nepal
        List.Add(new CodeAndFlag() { PhoneCode = "+98", FlagName = "iran" });//Iran
        List.Add(new CodeAndFlag() { PhoneCode = "+992", FlagName = "tajikistan" });//Tajikistan
        List.Add(new CodeAndFlag() { PhoneCode = "+993", FlagName = "turkmenistan" });//Turkmenistan
        List.Add(new CodeAndFlag() { PhoneCode = "+994", FlagName = "azerbaijan" });//Azerbaijan
        List.Add(new CodeAndFlag() { PhoneCode = "+996", FlagName = "kyrgyzstan" });//Kyrgyzstan
        List.Add(new CodeAndFlag() { PhoneCode = "+998", FlagName = "uzbekistan" });//Uzbekistan
    }
}

[Serializable]
public class CodeAndFlag
{
    public string PhoneCode;
    public string FlagName;
}
