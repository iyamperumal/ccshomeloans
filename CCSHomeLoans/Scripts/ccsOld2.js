//'use strict';



//Debt Consolidation: Pay Off Creditors  LoanTypeRequested



function LoanTypeChanged2() {
    if ($('#LoanTypeRequested').length === 0) {
        return;
    }
    var selecteditem = $("#LoanTypeRequested option:selected").text();

    $('#RealtorID').rules("add", { required: true, messages: { required: "Select Not Listed if your realtor is not in the list" } });
    //$("#RealtorID" ).rules( "add", {required: true, min: 1, messages: { required: "Select Not Listed if your realtor is not in the list"}});

    //Realtor Referred Purchase Loan

    if (selecteditem === "Realtor Referred Purchase Loan") {
        $('#realtorId').show(600);
        $('#RealtorID').rules("add", { required: true, messages: { required: "Select Not Listed if your realtor is not in the list" } });

    }
    else {

        $('#realtorId').hide(600);
        $('#RealtorID').rules("remove", "required");

    }


    if (selecteditem === "Purchase Loan" | selecteditem === "Realtor Referred Purchase Loan") {
        $('#purshasePriceId').show(600); //PurchasePrice
        $("#PurchasePrice").rules("add", { required: true, messages: { required: "Enter purshase price" } });

        $('#estimatedHoaId').show(600);
        $("#EstimatedHomeownersAssociationFeesAnnual").rules("add", { required: true, messages: { required: "Enter HOA or 0 if you dont have one" } });


        $('#downPaymentId').show(600);
        $("#DownPaymentAmount").rules("add", { required: true, messages: { required: "Enter down payment enter 0 if none" } });

        //SellerPaidColsingCost
        $('#SellerPaidColsingCostId').show(600);
        $("#SellerPaidCreditClosingCost").rules("add", { required: true, messages: { required: "Enter 0 if none" } });

    }
    else {

        $('#purshasePriceId').hide(600);
        $("#PurchasePrice").rules("remove", "required");

        $('#estimatedHoaId').hide(600);
        $("#EstimatedHomeownersAssociationFeesAnnual").rules("remove", "required");

        $('#downPaymentId').hide(600);
        $("#DownPaymentAmount").rules("remove", "required");

        $('#SellerPaidColsingCostId').hide(600);
        $("#SellerPaidCreditClosingCost").rules("remove", "required");
    }



    if (selecteditem === "Debt Consolidation: Pay Off Creditors") {
        $('#estimatedDebtToPayId').show(600);
        $("#EstimateTotalDebtToPayOff").rules("add", { required: true, messages: { required: "Enter you estimate for the total debt to pay off" } });

        $('#totalPmttopayOffId').show(600);
        $("#TotalOfMonthlyPaymentsOnDebtToPayOff").rules("add", { required: true, messages: { required: "Enter Total monthly Payment on debt to pay off" } });

        $('#additionalCashOutId').show(600);
        $("#AdditionalCashOutRequested").rules("add", { required: true, messages: { required: "Any additional Cash out 0 if none" } });


    }
    else {

        $('#estimatedDebtToPayId').hide(600);
        $("#EstimateTotalDebtToPayOff").rules("remove", "required");

        $('#totalPmttopayOffId').hide(600);
        $("#TotalOfMonthlyPaymentsOnDebtToPayOff").rules("remove", "required");

        $('#additionalCashOutId').hide(600);
        $("#AdditionalCashOutRequested").rules("remove", "required");

    }



    if (selecteditem === "Cash Out Mortgage") {
        $('#CashoutId').show(600);
        $("#CashOutRequested").rules("add", { required: true, messages: { required: "Enter the Cash Out you are requesting" } });
    }

    else {
        $('#CashoutId').hide(600);
        $("#CashOutRequested").rules("remove", "required");
    }


};






//No
//Chapter 7 Discharged
//Chapter 13 repayment
//Chapter 13 Discharged

function FiledBankruptcy() {


    var selecteditem2 = $("#filedBankruptcyId option:selected").text();



    if (selecteditem2 === "Chapter 7 Discharged" | selecteditem2 === "Chapter 13 Discharged") {
        $('#dateOfbankruptcyId').show(600);
        $("#BankruptcyDischargeDate").rules("add", { required: true, messages: { required: "Enter a Date" } });
    }

    else {
        $('#dateOfbankruptcyId').hide(600);
        $("#BankruptcyDischargeDate").rules("remove", "required");

    }



    if (selecteditem2 === "Chapter 13 Repayment Still Open") {
        $('#Chapter13Id').show(600);
        $("#Chapter13FilingDate").rules("add", { required: true, messages: { required: "Enter a Date" } });
    }

    else {
        $('#Chapter13Id').hide(600);
        $("#Chapter13FilingDate").rules("remove", "required");
    }

};

//Anyforeclosures

function AnyForeclosures() {

    var selecteditem3 = $("#ForeclosuresShortSaleDeedinLieu option:selected").text();


    if (selecteditem3 === "Yes Forclosure" | selecteditem3 === "Yes Short Sale" | selecteditem3 === "Yes Deed and Lieu") {
        $('#dateofForeclosureId').show(600);
        $("#ForeclosureShortSaleDeedinLieuDate").rules("add", { required: true, messages: { required: "Enter a Date" } });
    }

    else {
        $('#dateofForeclosureId').hide(600);
        $("#ForeclosureShortSaleDeedinLieuDate").rules("remove", "required");
    }


};

function App1Click(e) {

    $('#app1').slideToggle('slow');
    $('#app2').slideToggle('slow');

};


function App2Click(e) {


    var selecteditem4 = $("#LoanTypeRequested option:selected").text();

    if (selecteditem4 === "Purchase Loan" | selecteditem4 === "Realtor Referred Purchase Loan") {

        $('#app2').slideToggle('slow');
        $('#app5').slideToggle('slow');
    }
    else {
        $('#app2').slideToggle('slow');
        $('#app3').slideToggle('slow');
    }


    // $('#app1').hide('slide',{direction:'left'},'1000');
    // $('#app2').show('slide', { direction: 'left' });

};

function Back2Click() {


    $('#app2').slideToggle('slow');
    $('#app1').slideToggle('slow');


};


function App3Click(e) {


    $('#app3').slideToggle('slow');
    $('#app4').slideToggle('slow');


};

function Back3Click(e) {


    $('#app3').slideToggle('slow');
    $('#app2').slideToggle('slow');


};


function App4Click(e) {



    //$('#app1').hide(600);
    //$('#app2').show(600);

    $('#app4').slideToggle('slow');
    $('#app5').slideToggle('slow');


    // $('#app1').hide('slide',{direction:'left'},'1000');
    // $('#app2').show('slide', { direction: 'left' });

};

function Back4Click(e) {

    $('#app3').slideToggle('slow');
    $('#app4').slideToggle('slow');

};

function Back5Click(e) {
    var selecteditem5 = $("#LoanTypeRequested option:selected").text();

    if (selecteditem5 === "Purchase Loan" | selecteditem5 === "Realtor Referred Purchase Loan") {

        $('#app2').slideToggle('slow');
        $('#app5').slideToggle('slow');
    }
    else {
        $('#app4').slideToggle('slow');
        $('#app5').slideToggle('slow');
    }
};

function SecondMortgageChanged() {

    if ($('#Has2ndMortgage').length === 0) {
        return;
    }

    var secondmgitem = $("#Has2ndMortgage option:selected").text();


    if (secondmgitem === "Yes: Used to Purchase Home" || secondmgitem === "Yes: Aquired After Purchase") {

        $('#payOff2ndId').show(600);
        $("#PayOff2ndMortgage").rules("add", { required: true, messages: { required: "Pay Off 2nd Mortgage?" } });

        $('#secondMgBalId').show(600);
        $("#SecondMortgageBalance").rules("add", { required: true, messages: { required: "Enter your 2nd Mortgage balance" } });

        $('#secondMgInterestId').show(600);
        $("#SecondMortgageInterestRate").rules("add", { required: true, messages: { required: "Enter your 2nd Mortgage interest" } });

        $('#secondmgTypeId').show(600);
        $("#SecondMortgageRateType").rules("add", { required: true, messages: { required: "Select your 2nd Mortgage type" } });

        $('#secondMgTermId').show(600);
        $("#SecondMortgageTerm").rules("add", { required: true, messages: { required: "Select your Mortgage term" } });

        $('#secondMgPymtId').show(600);
        $("#SecondMortgagePayment").rules("add", { required: true, messages: { required: "Enter you 2nd Motgage Payment" } });

        //SecondMortgageOriginationDate

        $('#SecondMortgageOriginationDateId').show(600);
        $("#SecondMortgageOriginationDate").rules("add", { required: true, messages: { required: "Date you started the 2nd Mortgage" } });
    }
    else {
        $('#payOff2ndId').hide(600);
        $("#PayOff2ndMortgage").rules("remove", "required");

        $('#secondMgBalId').hide(600);
        $("#SecondMortgageBalance").rules("remove", "required");

        $('#secondMgInterestId').hide(600);
        $("#SecondMortgageInterestRate").rules("remove", "required");

        $('#secondmgTypeId').hide(600);
        $("#SecondMortgageRateType").rules("remove", "required");

        $('#secondMgTermId').hide(600);
        $("#SecondMortgageTerm").rules("remove", "required");

        $('#secondMgPymtId').hide(600);
        $("#SecondMortgagePayment").rules("remove", "required");

        $('#SecondMortgageOriginationDateId').hide(600);
        $("#SecondMortgageOriginationDate").rules("remove", "required");
    }

};

//homeownersAssocId
function MortgageInsChk() {

    if ($('#PymtIncludesMI').length === 0) {
        return;
    }

    if ($('#PymtIncludesMI').is(':checked')) {
        $('#MonthlyMortgageInsurId').show(600);
        $("#MonthlyMortgageInsur").rules("add", { required: true, messages: { required: "Enter Monthly Mortgage Ins Amount" } });
    }
    else {
        $('#MonthlyMortgageInsurId').hide(600);
        $("#MonthlyMortgageInsur").rules("remove", "required");
    }

};


function HoaChk() {
    if ($('#hoachkId').is(':checked')) {
        $('#homeownersAssocId').show(600);
    }
    else {
        $('#homeownersAssocId').hide(600);
    }
};

function appBegin() {
    var targetArea = $('#app');
    targetArea.slideToggle('slow');
};

function app1Success() {

    var form = $('#form0')
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(form);

    App2Loanded();
    //OwnerShipLongevity();
    LoanTypeChanged2();
    //setCounties();

    appBegin();
    Animprog("25%");

};

function App2Loanded() {
    //OwnerShipLongevity
    if ($('#OwnerShipLongevity').length === 0) {

        return;
    }
    AnyForeclosures();
    FiledBankruptcy();

    //Animprog("10%");
};

function app2Success() {

    var form = $('#form0')
           .removeData("validator") /* added by the raw jquery.validate plugin */
           .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(form);
    form.validate({ ignore: ":not(:visible)" });
    //$.validator.unobtrusive.parse(document);
    App2Loanded();
    LoanTypeChanged2();
    SecondMortgageChanged();
    LoanTypeChanged2();
    OnCountyChanged();

    appBegin();
    if ($('#LoanTypeRequested').length === 1) {
        Animprog("10%");
    }
    else if ($('#FirstName').length === 1) {
        Animprog("95%");
    }
    else {
        Animprog("50%");
    }

    //else if ($('#OwnerShipLongevity').length === 1) {
    //    Animprog("50%");
    //}

};


//app3Success
function app3Success() {

    var form = $('#form0')
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(form);

    App2Loanded();
    SecondMortgageChanged();


    appBegin();

    //OwnerShipLongevity
    if ($('#OwnerShipLongevity').length === 1) {
        Animprog("25%");
    }
    else {
        Animprog("70%");
    }
};


function app4Success() {

    var form = $('#form0')
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(form);

    //App2Loanded();
    SecondMortgageChanged();

    appBegin();
    if ($('#EstimatedHomeValue').length === 1) {
        Animprog("50%");
    }
    else {
        Animprog("90%");
    }
};

function app5Success() {

    var form = $('#form0')
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(form);
    MortgageInsChk();
    App2Loanded();
    //App2Loanded();
    //SecondMortgageChanged();
    //FirstMortgageOriginationDate


    appBegin();

    if ($('#FirstMortgageOriginationDate').length === 1) {
        Animprog("70%");
    }
    //else {
    //    Animprog("70%");
    //}
};

function Animprog(v) {
    //$('#appProg').animate({aria-valuenow : v}, 2000);
    $('#appProg').animate({ width: v });
};


//=============================================




//jQuery(function ($) {
//    var locations = {
//        'Germany': [{ fips: 1, name: "Duesseldorf" }, { fips: 2, name: "Leinfelden-Echterdingen" }, { fips: 3, name: "Eschborn" }],
//        'USA': [{ fips: 4, name: "Downers Grove" }, { fips: 17, name: "florida" }]
//    };

//    var $locations = $('#location');
//    $('#country').change(function () {
//        var country = $(this).val(), lcns = locations[country] || [];

//        var html = $.map(lcns, function (lcn) {
//            return '<option value="' + lcn.fips + '">' + lcn.name + '</option>';
//        }).join('');
//        $locations.html(html);
//    });
//});



function OnCountyChanged() {

    if ($('#CountyFips').length === 0) {
        return;
    }

    var locations = {
        'Alabama': [{ fips: 1, name: 'Autauga' }, { fips: 3, name: 'Baldwin' }, { fips: 5, name: 'Barbour' }, { fips: 7, name: 'Bibb' }, { fips: 9, name: 'Blount' }, { fips: 11, name: 'Bullock' }, { fips: 13, name: 'Butler' }, { fips: 15, name: 'Calhoun' }, { fips: 17, name: 'Chambers' }, { fips: 19, name: 'Cherokee' }, { fips: 21, name: 'Chilton' }, { fips: 23, name: 'Choctaw' }, { fips: 25, name: 'Clarke' }, { fips: 27, name: 'Clay' }, { fips: 29, name: 'Cleburne' }, { fips: 31, name: 'Coffee' }, { fips: 33, name: 'Colbert' }, { fips: 35, name: 'Conecuh' }, { fips: 37, name: 'Coosa' }, { fips: 39, name: 'Covington' }, { fips: 41, name: 'Crenshaw' }, { fips: 43, name: 'Cullman' }, { fips: 45, name: 'Dale' }, { fips: 47, name: 'Dallas' }, { fips: 49, name: 'De Kalb' }, { fips: 51, name: 'Elmore' }, { fips: 53, name: 'Escambia' }, { fips: 55, name: 'Etowah' }, { fips: 57, name: 'Fayette' }, { fips: 59, name: 'Franklin' }, { fips: 61, name: 'Geneva' }, { fips: 63, name: 'Greene' }, { fips: 65, name: 'Hale' }, { fips: 67, name: 'Henry' }, { fips: 69, name: 'Houston' }, { fips: 71, name: 'Jackson' }, { fips: 73, name: 'Jefferson' }, { fips: 75, name: 'Lamar' }, { fips: 77, name: 'Lauderdale' }, { fips: 79, name: 'Lawrence' }, { fips: 81, name: 'Lee' }, { fips: 83, name: 'Limestone' }, { fips: 85, name: 'Lowndes' }, { fips: 87, name: 'Macon' }, { fips: 89, name: 'Madison' }, { fips: 91, name: 'Marengo' }, { fips: 93, name: 'Marion' }, { fips: 95, name: 'Marshall' }, { fips: 97, name: 'Mobile' }, { fips: 99, name: 'Monroe' }, { fips: 101, name: 'Montgomery' }, { fips: 103, name: 'Morgan' }, { fips: 105, name: 'Perry' }, { fips: 107, name: 'Pickens' }, { fips: 109, name: 'Pike' }, { fips: 111, name: 'Randolph' }, { fips: 113, name: 'Russell' }, { fips: 115, name: 'St Clair' }, { fips: 117, name: 'Shelby' }, { fips: 119, name: 'Sumter' }, { fips: 121, name: 'Talladega' }, { fips: 123, name: 'Tallapoosa' }, { fips: 125, name: 'Tuscaloosa' }, { fips: 127, name: 'Walker' }, { fips: 129, name: 'Washington' }, { fips: 131, name: 'Wilcox' }, { fips: 133, name: 'Winston' }],
        'Alaska': [{ fips: 13, name: 'Aleutians East' }, { fips: 16, name: 'Aleutians West' }, { fips: 20, name: 'Anchorage' }, { fips: 50, name: 'Bethel' }, { fips: 60, name: 'Bristol Bay' }, { fips: 68, name: 'Denali' }, { fips: 70, name: 'Dillingham' }, { fips: 90, name: 'Fairbanks North Star' }, { fips: 100, name: 'Haines' }, { fips: 110, name: 'Juneau' }, { fips: 122, name: 'Kenai Peninsula' }, { fips: 130, name: 'Ketchikan Gateway' }, { fips: 150, name: 'Kodiak Island' }, { fips: 164, name: 'Lake and Peninsula' }, { fips: 170, name: 'Matanuska Susitna' }, { fips: 180, name: 'Nome' }, { fips: 185, name: 'North Slope' }, { fips: 188, name: 'Northwest Arctic' }, { fips: 201, name: 'Prince Wales Ketchikan' }, { fips: 220, name: 'Sitka' }, { fips: 232, name: 'Skagway Hoonah Angoon' }, { fips: 240, name: 'Southeast Fairbanks' }, { fips: 261, name: 'Valdez Cordova' }, { fips: 270, name: 'Wade Hampton' }, { fips: 280, name: 'Wrangell Petersburg' }, { fips: 282, name: 'Yakutat' }, { fips: 290, name: 'Yukon Koyukuk' }],
        'Arizona': [{ fips: 1, name: 'Apache' }, { fips: 3, name: 'Cochise' }, { fips: 5, name: 'Coconino' }, { fips: 7, name: 'Gila' }, { fips: 9, name: 'Graham' }, { fips: 11, name: 'Greenlee' }, { fips: 12, name: 'La Paz' }, { fips: 13, name: 'Maricopa' }, { fips: 15, name: 'Mohave' }, { fips: 17, name: 'Navajo' }, { fips: 19, name: 'Pima' }, { fips: 21, name: 'Pinal' }, { fips: 23, name: 'Santa Cruz' }, { fips: 25, name: 'Yavapai' }, { fips: 27, name: 'Yuma' }],
        'Arkansas': [{ fips: 1, name: 'Arkansas' }, { fips: 3, name: 'Ashley' }, { fips: 5, name: 'Baxter' }, { fips: 7, name: 'Benton' }, { fips: 9, name: 'Boone' }, { fips: 11, name: 'Bradley' }, { fips: 13, name: 'Calhoun' }, { fips: 15, name: 'Carroll' }, { fips: 17, name: 'Chicot' }, { fips: 19, name: 'Clark' }, { fips: 21, name: 'Clay' }, { fips: 23, name: 'Cleburne' }, { fips: 25, name: 'Cleveland' }, { fips: 27, name: 'Columbia' }, { fips: 29, name: 'Conway' }, { fips: 31, name: 'Craighead' }, { fips: 33, name: 'Crawford' }, { fips: 35, name: 'Crittenden' }, { fips: 37, name: 'Cross' }, { fips: 39, name: 'Dallas' }, { fips: 41, name: 'Desha' }, { fips: 43, name: 'Drew' }, { fips: 45, name: 'Faulkner' }, { fips: 47, name: 'Franklin' }, { fips: 49, name: 'Fulton' }, { fips: 51, name: 'Garland' }, { fips: 53, name: 'Grant' }, { fips: 55, name: 'Greene' }, { fips: 57, name: 'Hempstead' }, { fips: 59, name: 'Hot Spring' }, { fips: 61, name: 'Howard' }, { fips: 63, name: 'Independence' }, { fips: 65, name: 'Izard' }, { fips: 67, name: 'Jackson' }, { fips: 69, name: 'Jefferson' }, { fips: 71, name: 'Johnson' }, { fips: 73, name: 'Lafayette' }, { fips: 75, name: 'Lawrence' }, { fips: 77, name: 'Lee' }, { fips: 79, name: 'Lincoln' }, { fips: 81, name: 'Little River' }, { fips: 83, name: 'Logan' }, { fips: 85, name: 'Lonoke' }, { fips: 87, name: 'Madison' }, { fips: 89, name: 'Marion' }, { fips: 91, name: 'Miller' }, { fips: 93, name: 'Mississippi' }, { fips: 95, name: 'Monroe' }, { fips: 99, name: 'Nevada' }, { fips: 101, name: 'Newton' }, { fips: 103, name: 'Ouachita' }, { fips: 105, name: 'Perry' }, { fips: 107, name: 'Phillips' }, { fips: 109, name: 'Pike' }, { fips: 111, name: 'Poinsett' }, { fips: 113, name: 'Polk' }, { fips: 115, name: 'Pope' }, { fips: 117, name: 'Prairie' }, { fips: 119, name: 'Pulaski' }, { fips: 121, name: 'Randolph' }, { fips: 123, name: 'St Francis' }, { fips: 125, name: 'Saline' }, { fips: 127, name: 'Scott' }, { fips: 129, name: 'Searcy' }, { fips: 131, name: 'Sebastian' }, { fips: 133, name: 'Sevier' }, { fips: 135, name: 'Sharp' }, { fips: 137, name: 'Stone' }, { fips: 139, name: 'Union' }, { fips: 141, name: 'Van Buren' }, { fips: 143, name: 'Washington' }, { fips: 145, name: 'White' }, { fips: 147, name: 'Woodruff' }, { fips: 149, name: 'Yell' }],
        'California': [{ fips: 1, name: 'Alameda' }, { fips: 3, name: 'Alpine' }, { fips: 5, name: 'Amador' }, { fips: 7, name: 'Butte' }, { fips: 9, name: 'Calaveras' }, { fips: 11, name: 'Colusa' }, { fips: 13, name: 'Contra Costa' }, { fips: 15, name: 'Del Norte' }, { fips: 17, name: 'El Dorado' }, { fips: 19, name: 'Fresno' }, { fips: 21, name: 'Glenn' }, { fips: 23, name: 'Humboldt' }, { fips: 25, name: 'Imperial' }, { fips: 27, name: 'Inyo' }, { fips: 29, name: 'Kern' }, { fips: 31, name: 'Kings' }, { fips: 33, name: 'Lake' }, { fips: 35, name: 'Lassen' }, { fips: 37, name: 'Los Angeles' }, { fips: 39, name: 'Madera' }, { fips: 41, name: 'Marin' }, { fips: 43, name: 'Mariposa' }, { fips: 45, name: 'Mendocino' }, { fips: 47, name: 'Merced' }, { fips: 49, name: 'Modoc' }, { fips: 51, name: 'Mono' }, { fips: 53, name: 'Monterey' }, { fips: 55, name: 'Napa' }, { fips: 57, name: 'Nevada' }, { fips: 59, name: 'Orange' }, { fips: 61, name: 'Placer' }, { fips: 63, name: 'Plumas' }, { fips: 65, name: 'Riverside' }, { fips: 67, name: 'Sacramento' }, { fips: 69, name: 'San Benito' }, { fips: 71, name: 'San Bernardino' }, { fips: 73, name: 'San Diego' }, { fips: 75, name: 'San Francisco' }, { fips: 77, name: 'San Joaquin' }, { fips: 79, name: 'San Luis Obispo' }, { fips: 81, name: 'San Mateo' }, { fips: 83, name: 'Santa Barbara' }, { fips: 85, name: 'Santa Clara' }, { fips: 87, name: 'Santa Cruz' }, { fips: 89, name: 'Shasta' }, { fips: 91, name: 'Sierra' }, { fips: 93, name: 'Siskiyou' }, { fips: 95, name: 'Solano' }, { fips: 97, name: 'Sonoma' }, { fips: 99, name: 'Stanislaus' }, { fips: 101, name: 'Sutter' }, { fips: 103, name: 'Tehama' }, { fips: 105, name: 'Trinity' }, { fips: 107, name: 'Tulare' }, { fips: 109, name: 'Tuolumne' }, { fips: 111, name: 'Ventura' }, { fips: 113, name: 'Yolo' }, { fips: 115, name: 'Yuba' }],
        'Colorado': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Alamosa' }, { fips: 5, name: 'Arapahoe' }, { fips: 7, name: 'Archuleta' }, { fips: 9, name: 'Baca' }, { fips: 11, name: 'Bent' }, { fips: 13, name: 'Boulder' }, { fips: 14, name: 'Broomfield' }, { fips: 15, name: 'Chaffee' }, { fips: 17, name: 'Cheyenne' }, { fips: 19, name: 'Clear Creek' }, { fips: 21, name: 'Conejos' }, { fips: 23, name: 'Costilla' }, { fips: 25, name: 'Crowley' }, { fips: 27, name: 'Custer' }, { fips: 29, name: 'Delta' }, { fips: 31, name: 'Denver' }, { fips: 33, name: 'Dolores' }, { fips: 35, name: 'Douglas' }, { fips: 37, name: 'Eagle' }, { fips: 39, name: 'Elbert' }, { fips: 41, name: 'El Paso' }, { fips: 43, name: 'Fremont' }, { fips: 45, name: 'Garfield' }, { fips: 47, name: 'Gilpin' }, { fips: 49, name: 'Grand' }, { fips: 51, name: 'Gunnison' }, { fips: 53, name: 'Hinsdale' }, { fips: 55, name: 'Huerfano' }, { fips: 57, name: 'Jackson' }, { fips: 59, name: 'Jefferson' }, { fips: 61, name: 'Kiowa' }, { fips: 63, name: 'Kit Carson' }, { fips: 65, name: 'Lake' }, { fips: 67, name: 'La Plata' }, { fips: 69, name: 'Larimer' }, { fips: 71, name: 'Las Animas' }, { fips: 73, name: 'Lincoln' }, { fips: 75, name: 'Logan' }, { fips: 77, name: 'Mesa' }, { fips: 79, name: 'Mineral' }, { fips: 81, name: 'Moffat' }, { fips: 83, name: 'Montezuma' }, { fips: 85, name: 'Montrose' }, { fips: 87, name: 'Morgan' }, { fips: 89, name: 'Otero' }, { fips: 91, name: 'Ouray' }, { fips: 93, name: 'Park' }, { fips: 95, name: 'Phillips' }, { fips: 97, name: 'Pitkin' }, { fips: 99, name: 'Prowers' }, { fips: 101, name: 'Pueblo' }, { fips: 103, name: 'Rio Blanco' }, { fips: 105, name: 'Rio Grande' }, { fips: 107, name: 'Routt' }, { fips: 109, name: 'Saguache' }, { fips: 111, name: 'San Juan' }, { fips: 113, name: 'San Miguel' }, { fips: 115, name: 'Sedgwick' }, { fips: 117, name: 'Summit' }, { fips: 119, name: 'Teller' }, { fips: 121, name: 'Washington' }, { fips: 123, name: 'Weld' }, { fips: 125, name: 'Yuma' }],
        'Connecticut': [{ fips: 1, name: 'Fairfield' }, { fips: 3, name: 'Hartford' }, { fips: 5, name: 'Litchfield' }, { fips: 7, name: 'Middlesex' }, { fips: 9, name: 'New Haven' }, { fips: 11, name: 'New London' }, { fips: 13, name: 'Tolland' }, { fips: 15, name: 'Windham' }],
        'Delaware': [{ fips: 1, name: 'Kent' }, { fips: 3, name: 'New Castle' }, { fips: 5, name: 'Sussex' }],
        'District of Columbia': [{ fips: 1, name: 'District of Columbia' }, { fips: 31, name: 'Montgomery' }],
        'Florida': [{ fips: 1, name: 'Alachua' }, { fips: 3, name: 'Baker' }, { fips: 5, name: 'Bay' }, { fips: 7, name: 'Bradford' }, { fips: 9, name: 'Brevard' }, { fips: 11, name: 'Broward' }, { fips: 13, name: 'Calhoun' }, { fips: 15, name: 'Charlotte' }, { fips: 17, name: 'Citrus' }, { fips: 19, name: 'Clay' }, { fips: 21, name: 'Collier' }, { fips: 23, name: 'Columbia' }, { fips: 27, name: 'De Soto' }, { fips: 29, name: 'Dixie' }, { fips: 31, name: 'Duval' }, { fips: 33, name: 'Escambia' }, { fips: 35, name: 'Flagler' }, { fips: 37, name: 'Franklin' }, { fips: 39, name: 'Gadsden' }, { fips: 41, name: 'Gilchrist' }, { fips: 43, name: 'Glades' }, { fips: 45, name: 'Gulf' }, { fips: 47, name: 'Hamilton' }, { fips: 49, name: 'Hardee' }, { fips: 51, name: 'Hendry' }, { fips: 53, name: 'Hernando' }, { fips: 55, name: 'Highlands' }, { fips: 57, name: 'Hillsborough' }, { fips: 59, name: 'Holmes' }, { fips: 61, name: 'Indian River' }, { fips: 63, name: 'Jackson' }, { fips: 65, name: 'Jefferson' }, { fips: 67, name: 'Lafayette' }, { fips: 69, name: 'Lake' }, { fips: 71, name: 'Lee' }, { fips: 73, name: 'Leon' }, { fips: 75, name: 'Levy' }, { fips: 77, name: 'Liberty' }, { fips: 79, name: 'Madison' }, { fips: 81, name: 'Manatee' }, { fips: 83, name: 'Marion' }, { fips: 85, name: 'Martin' }, { fips: 86, name: 'Miami-Dade' }, { fips: 87, name: 'Monroe' }, { fips: 89, name: 'Nassau' }, { fips: 91, name: 'Okaloosa' }, { fips: 93, name: 'Okeechobee' }, { fips: 95, name: 'Orange' }, { fips: 97, name: 'Osceola' }, { fips: 99, name: 'Palm Beach' }, { fips: 101, name: 'Pasco' }, { fips: 103, name: 'Pinellas' }, { fips: 105, name: 'Polk' }, { fips: 107, name: 'Putnam' }, { fips: 109, name: 'St Johns' }, { fips: 111, name: 'St Lucie' }, { fips: 113, name: 'Santa Rosa' }, { fips: 115, name: 'Sarasota' }, { fips: 117, name: 'Seminole' }, { fips: 119, name: 'Sumter' }, { fips: 121, name: 'Suwannee' }, { fips: 123, name: 'Taylor' }, { fips: 125, name: 'Union' }, { fips: 127, name: 'Volusia' }, { fips: 129, name: 'Wakulla' }, { fips: 131, name: 'Walton' }, { fips: 133, name: 'Washington' }],
        'Georgia': [{ fips: 1, name: 'Appling' }, { fips: 3, name: 'Atkinson' }, { fips: 5, name: 'Bacon' }, { fips: 7, name: 'Baker' }, { fips: 9, name: 'Baldwin' }, { fips: 11, name: 'Banks' }, { fips: 13, name: 'Barrow' }, { fips: 15, name: 'Bartow' }, { fips: 17, name: 'Ben Hill' }, { fips: 19, name: 'Berrien' }, { fips: 21, name: 'Bibb' }, { fips: 23, name: 'Bleckley' }, { fips: 25, name: 'Brantley' }, { fips: 27, name: 'Brooks' }, { fips: 29, name: 'Bryan' }, { fips: 31, name: 'Bulloch' }, { fips: 33, name: 'Burke' }, { fips: 35, name: 'Butts' }, { fips: 37, name: 'Calhoun' }, { fips: 39, name: 'Camden' }, { fips: 43, name: 'Candler' }, { fips: 45, name: 'Carroll' }, { fips: 47, name: 'Catoosa' }, { fips: 49, name: 'Charlton' }, { fips: 51, name: 'Chatham' }, { fips: 53, name: 'Chattahoochee' }, { fips: 55, name: 'Chattooga' }, { fips: 57, name: 'Cherokee' }, { fips: 59, name: 'Clarke' }, { fips: 61, name: 'Clay' }, { fips: 63, name: 'Clayton' }, { fips: 65, name: 'Clinch' }, { fips: 67, name: 'Cobb' }, { fips: 69, name: 'Coffee' }, { fips: 71, name: 'Colquitt' }, { fips: 73, name: 'Columbia' }, { fips: 75, name: 'Cook' }, { fips: 77, name: 'Coweta' }, { fips: 79, name: 'Crawford' }, { fips: 81, name: 'Crisp' }, { fips: 83, name: 'Dade' }, { fips: 85, name: 'Dawson' }, { fips: 87, name: 'Decatur' }, { fips: 89, name: 'De Kalb' }, { fips: 91, name: 'Dodge' }, { fips: 93, name: 'Dooly' }, { fips: 95, name: 'Dougherty' }, { fips: 97, name: 'Douglas' }, { fips: 99, name: 'Early' }, { fips: 101, name: 'Echols' }, { fips: 103, name: 'Effingham' }, { fips: 105, name: 'Elbert' }, { fips: 107, name: 'Emanuel' }, { fips: 109, name: 'Evans' }, { fips: 111, name: 'Fannin' }, { fips: 113, name: 'Fayette' }, { fips: 115, name: 'Floyd' }, { fips: 117, name: 'Forsyth' }, { fips: 119, name: 'Franklin' }, { fips: 121, name: 'Fulton' }, { fips: 123, name: 'Gilmer' }, { fips: 125, name: 'Glascock' }, { fips: 127, name: 'Glynn' }, { fips: 129, name: 'Gordon' }, { fips: 131, name: 'Grady' }, { fips: 133, name: 'Greene' }, { fips: 135, name: 'Gwinnett' }, { fips: 137, name: 'Habersham' }, { fips: 139, name: 'Hall' }, { fips: 141, name: 'Hancock' }, { fips: 143, name: 'Haralson' }, { fips: 145, name: 'Harris' }, { fips: 147, name: 'Hart' }, { fips: 149, name: 'Heard' }, { fips: 151, name: 'Henry' }, { fips: 153, name: 'Houston' }, { fips: 155, name: 'Irwin' }, { fips: 157, name: 'Jackson' }, { fips: 159, name: 'Jasper' }, { fips: 161, name: 'Jeff Davis' }, { fips: 163, name: 'Jefferson' }, { fips: 165, name: 'Jenkins' }, { fips: 167, name: 'Johnson' }, { fips: 169, name: 'Jones' }, { fips: 171, name: 'Lamar' }, { fips: 173, name: 'Lanier' }, { fips: 175, name: 'Laurens' }, { fips: 177, name: 'Lee' }, { fips: 179, name: 'Liberty' }, { fips: 181, name: 'Lincoln' }, { fips: 183, name: 'Long' }, { fips: 185, name: 'Lowndes' }, { fips: 187, name: 'Lumpkin' }, { fips: 189, name: 'McDuffie' }, { fips: 191, name: 'McIntosh' }, { fips: 193, name: 'Macon' }, { fips: 195, name: 'Madison' }, { fips: 197, name: 'Marion' }, { fips: 199, name: 'Meriwether' }, { fips: 201, name: 'Miller' }, { fips: 205, name: 'Mitchell' }, { fips: 207, name: 'Monroe' }, { fips: 209, name: 'Montgomery' }, { fips: 211, name: 'Morgan' }, { fips: 213, name: 'Murray' }, { fips: 215, name: 'Muscogee' }, { fips: 217, name: 'Newton' }, { fips: 219, name: 'Oconee' }, { fips: 221, name: 'Oglethorpe' }, { fips: 223, name: 'Paulding' }, { fips: 225, name: 'Peach' }, { fips: 227, name: 'Pickens' }, { fips: 229, name: 'Pierce' }, { fips: 231, name: 'Pike' }, { fips: 233, name: 'Polk' }, { fips: 235, name: 'Pulaski' }, { fips: 237, name: 'Putnam' }, { fips: 239, name: 'Quitman' }, { fips: 241, name: 'Rabun' }, { fips: 243, name: 'Randolph' }, { fips: 245, name: 'Richmond' }, { fips: 247, name: 'Rockdale' }, { fips: 249, name: 'Schley' }, { fips: 251, name: 'Screven' }, { fips: 253, name: 'Seminole' }, { fips: 255, name: 'Spalding' }, { fips: 257, name: 'Stephens' }, { fips: 259, name: 'Stewart' }, { fips: 261, name: 'Sumter' }, { fips: 263, name: 'Talbot' }, { fips: 265, name: 'Taliaferro' }, { fips: 267, name: 'Tattnall' }, { fips: 269, name: 'Taylor' }, { fips: 271, name: 'Telfair' }, { fips: 273, name: 'Terrell' }, { fips: 275, name: 'Thomas' }, { fips: 277, name: 'Tift' }, { fips: 279, name: 'Toombs' }, { fips: 281, name: 'Towns' }, { fips: 283, name: 'Treutlen' }, { fips: 285, name: 'Troup' }, { fips: 287, name: 'Turner' }, { fips: 289, name: 'Twiggs' }, { fips: 291, name: 'Union' }, { fips: 293, name: 'Upson' }, { fips: 295, name: 'Walker' }, { fips: 297, name: 'Walton' }, { fips: 299, name: 'Ware' }, { fips: 301, name: 'Warren' }, { fips: 303, name: 'Washington' }, { fips: 305, name: 'Wayne' }, { fips: 307, name: 'Webster' }, { fips: 309, name: 'Wheeler' }, { fips: 311, name: 'White' }, { fips: 313, name: 'Whitfield' }, { fips: 315, name: 'Wilcox' }, { fips: 317, name: 'Wilkes' }, { fips: 319, name: 'Wilkinson' }, { fips: 321, name: 'Worth' }],
        'Hawaii': [{ fips: 1, name: 'Hawaii' }, { fips: 3, name: 'Honolulu' }, { fips: 7, name: 'Kauai' }, { fips: 9, name: 'Maui' }],
        'Idaho': [{ fips: 1, name: 'Ada' }, { fips: 3, name: 'Adams' }, { fips: 5, name: 'Bannock' }, { fips: 7, name: 'Bear Lake' }, { fips: 9, name: 'Benewah' }, { fips: 11, name: 'Bingham' }, { fips: 13, name: 'Blaine' }, { fips: 15, name: 'Boise' }, { fips: 17, name: 'Bonner' }, { fips: 19, name: 'Bonneville' }, { fips: 21, name: 'Boundary' }, { fips: 23, name: 'Butte' }, { fips: 25, name: 'Camas' }, { fips: 27, name: 'Canyon' }, { fips: 29, name: 'Caribou' }, { fips: 31, name: 'Cassia' }, { fips: 33, name: 'Clark' }, { fips: 35, name: 'Clearwater' }, { fips: 37, name: 'Custer' }, { fips: 39, name: 'Elmore' }, { fips: 41, name: 'Franklin' }, { fips: 43, name: 'Fremont' }, { fips: 45, name: 'Gem' }, { fips: 47, name: 'Gooding' }, { fips: 49, name: 'Idaho' }, { fips: 51, name: 'Jefferson' }, { fips: 53, name: 'Jerome' }, { fips: 55, name: 'Kootenai' }, { fips: 57, name: 'Latah' }, { fips: 59, name: 'Lemhi' }, { fips: 61, name: 'Lewis' }, { fips: 63, name: 'Lincoln' }, { fips: 65, name: 'Madison' }, { fips: 67, name: 'Minidoka' }, { fips: 69, name: 'Nez Perce' }, { fips: 71, name: 'Oneida' }, { fips: 73, name: 'Owyhee' }, { fips: 75, name: 'Payette' }, { fips: 77, name: 'Power' }, { fips: 79, name: 'Shoshone' }, { fips: 81, name: 'Teton' }, { fips: 83, name: 'Twin Falls' }, { fips: 85, name: 'Valley' }, { fips: 87, name: 'Washington' }],
        'Illinois': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Alexander' }, { fips: 5, name: 'Bond' }, { fips: 7, name: 'Boone' }, { fips: 9, name: 'Brown' }, { fips: 11, name: 'Bureau' }, { fips: 13, name: 'Calhoun' }, { fips: 15, name: 'Carroll' }, { fips: 17, name: 'Cass' }, { fips: 19, name: 'Champaign' }, { fips: 21, name: 'Christian' }, { fips: 23, name: 'Clark' }, { fips: 25, name: 'Clay' }, { fips: 27, name: 'Clinton' }, { fips: 29, name: 'Coles' }, { fips: 31, name: 'Cook' }, { fips: 33, name: 'Crawford' }, { fips: 35, name: 'Cumberland' }, { fips: 37, name: 'De Kalb' }, { fips: 39, name: 'Dewitt' }, { fips: 41, name: 'Douglas' }, { fips: 43, name: 'Du Page' }, { fips: 45, name: 'Edgar' }, { fips: 47, name: 'Edwards' }, { fips: 49, name: 'Effingham' }, { fips: 51, name: 'Fayette' }, { fips: 53, name: 'Ford' }, { fips: 55, name: 'Franklin' }, { fips: 57, name: 'Fulton' }, { fips: 59, name: 'Gallatin' }, { fips: 61, name: 'Greene' }, { fips: 63, name: 'Grundy' }, { fips: 65, name: 'Hamilton' }, { fips: 67, name: 'Hancock' }, { fips: 69, name: 'Hardin' }, { fips: 71, name: 'Henderson' }, { fips: 73, name: 'Henry' }, { fips: 75, name: 'Iroquois' }, { fips: 77, name: 'Jackson' }, { fips: 79, name: 'Jasper' }, { fips: 81, name: 'Jefferson' }, { fips: 83, name: 'Jersey' }, { fips: 85, name: 'Jo Daviess' }, { fips: 87, name: 'Johnson' }, { fips: 89, name: 'Kane' }, { fips: 91, name: 'Kankakee' }, { fips: 93, name: 'Kendall' }, { fips: 95, name: 'Knox' }, { fips: 97, name: 'Lake' }, { fips: 99, name: 'La Salle' }, { fips: 101, name: 'Lawrence' }, { fips: 103, name: 'Lee' }, { fips: 105, name: 'Livingston' }, { fips: 107, name: 'Logan' }, { fips: 109, name: 'McDonough' }, { fips: 111, name: 'McHenry' }, { fips: 113, name: 'Mclean' }, { fips: 115, name: 'Macon' }, { fips: 117, name: 'Macoupin' }, { fips: 119, name: 'Madison' }, { fips: 121, name: 'Marion' }, { fips: 123, name: 'Marshall' }, { fips: 125, name: 'Mason' }, { fips: 127, name: 'Massac' }, { fips: 129, name: 'Menard' }, { fips: 131, name: 'Mercer' }, { fips: 133, name: 'Monroe' }, { fips: 135, name: 'Montgomery' }, { fips: 137, name: 'Morgan' }, { fips: 139, name: 'Moultrie' }, { fips: 141, name: 'Ogle' }, { fips: 143, name: 'Peoria' }, { fips: 145, name: 'Perry' }, { fips: 147, name: 'Piatt' }, { fips: 149, name: 'Pike' }, { fips: 151, name: 'Pope' }, { fips: 153, name: 'Pulaski' }, { fips: 155, name: 'Putnam' }, { fips: 157, name: 'Randolph' }, { fips: 159, name: 'Richland' }, { fips: 161, name: 'Rock Island' }, { fips: 163, name: 'St Clair' }, { fips: 165, name: 'Saline' }, { fips: 167, name: 'Sangamon' }, { fips: 169, name: 'Schuyler' }, { fips: 171, name: 'Scott' }, { fips: 173, name: 'Shelby' }, { fips: 175, name: 'Stark' }, { fips: 177, name: 'Stephenson' }, { fips: 179, name: 'Tazewell' }, { fips: 181, name: 'Union' }, { fips: 183, name: 'Vermilion' }, { fips: 185, name: 'Wabash' }, { fips: 187, name: 'Warren' }, { fips: 189, name: 'Washington' }, { fips: 191, name: 'Wayne' }, { fips: 193, name: 'White' }, { fips: 195, name: 'Whiteside' }, { fips: 197, name: 'Will' }, { fips: 199, name: 'Williamson' }, { fips: 201, name: 'Winnebago' }, { fips: 203, name: 'Woodford' }],
        'Indiana': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Allen' }, { fips: 5, name: 'Bartholomew' }, { fips: 7, name: 'Benton' }, { fips: 9, name: 'Blackford' }, { fips: 11, name: 'Boone' }, { fips: 13, name: 'Brown' }, { fips: 15, name: 'Carroll' }, { fips: 17, name: 'Cass' }, { fips: 19, name: 'Clark' }, { fips: 21, name: 'Clay' }, { fips: 23, name: 'Clinton' }, { fips: 25, name: 'Crawford' }, { fips: 27, name: 'Daviess' }, { fips: 29, name: 'Dearborn' }, { fips: 31, name: 'Decatur' }, { fips: 33, name: 'De Kalb' }, { fips: 35, name: 'Delaware' }, { fips: 37, name: 'Dubois' }, { fips: 39, name: 'Elkhart' }, { fips: 41, name: 'Fayette' }, { fips: 43, name: 'Floyd' }, { fips: 45, name: 'Fountain' }, { fips: 47, name: 'Franklin' }, { fips: 49, name: 'Fulton' }, { fips: 51, name: 'Gibson' }, { fips: 53, name: 'Grant' }, { fips: 55, name: 'Greene' }, { fips: 57, name: 'Hamilton' }, { fips: 59, name: 'Hancock' }, { fips: 61, name: 'Harrison' }, { fips: 63, name: 'Hendricks' }, { fips: 65, name: 'Henry' }, { fips: 67, name: 'Howard' }, { fips: 69, name: 'Huntington' }, { fips: 71, name: 'Jackson' }, { fips: 73, name: 'Jasper' }, { fips: 75, name: 'Jay' }, { fips: 77, name: 'Jefferson' }, { fips: 79, name: 'Jennings' }, { fips: 81, name: 'Johnson' }, { fips: 83, name: 'Knox' }, { fips: 85, name: 'Kosciusko' }, { fips: 87, name: 'Lagrange' }, { fips: 89, name: 'Lake' }, { fips: 91, name: 'La Porte' }, { fips: 93, name: 'Lawrence' }, { fips: 95, name: 'Madison' }, { fips: 97, name: 'Marion' }, { fips: 99, name: 'Marshall' }, { fips: 101, name: 'Martin' }, { fips: 103, name: 'Miami' }, { fips: 105, name: 'Monroe' }, { fips: 107, name: 'Montgomery' }, { fips: 109, name: 'Morgan' }, { fips: 111, name: 'Newton' }, { fips: 113, name: 'Noble' }, { fips: 115, name: 'Ohio' }, { fips: 117, name: 'Orange' }, { fips: 119, name: 'Owen' }, { fips: 121, name: 'Parke' }, { fips: 123, name: 'Perry' }, { fips: 125, name: 'Pike' }, { fips: 127, name: 'Porter' }, { fips: 129, name: 'Posey' }, { fips: 131, name: 'Pulaski' }, { fips: 133, name: 'Putnam' }, { fips: 135, name: 'Randolph' }, { fips: 137, name: 'Ripley' }, { fips: 139, name: 'Rush' }, { fips: 141, name: 'St Joseph' }, { fips: 143, name: 'Scott' }, { fips: 145, name: 'Shelby' }, { fips: 147, name: 'Spencer' }, { fips: 149, name: 'Starke' }, { fips: 151, name: 'Steuben' }, { fips: 153, name: 'Sullivan' }, { fips: 155, name: 'Switzerland' }, { fips: 157, name: 'Tippecanoe' }, { fips: 159, name: 'Tipton' }, { fips: 161, name: 'Union' }, { fips: 163, name: 'Vanderburgh' }, { fips: 165, name: 'Vermillion' }, { fips: 167, name: 'Vigo' }, { fips: 169, name: 'Wabash' }, { fips: 171, name: 'Warren' }, { fips: 173, name: 'Warrick' }, { fips: 175, name: 'Washington' }, { fips: 177, name: 'Wayne' }, { fips: 179, name: 'Wells' }, { fips: 181, name: 'White' }, { fips: 183, name: 'Whitley' }],
        'Iowa': [{ fips: 1, name: 'Adair' }, { fips: 3, name: 'Adams' }, { fips: 5, name: 'Allamakee' }, { fips: 7, name: 'Appanoose' }, { fips: 9, name: 'Audubon' }, { fips: 11, name: 'Benton' }, { fips: 13, name: 'Black Hawk' }, { fips: 15, name: 'Boone' }, { fips: 17, name: 'Bremer' }, { fips: 19, name: 'Buchanan' }, { fips: 21, name: 'Buena Vista' }, { fips: 23, name: 'Butler' }, { fips: 25, name: 'Calhoun' }, { fips: 27, name: 'Carroll' }, { fips: 29, name: 'Cass' }, { fips: 31, name: 'Cedar' }, { fips: 33, name: 'Cerro Gordo' }, { fips: 35, name: 'Cherokee' }, { fips: 37, name: 'Chickasaw' }, { fips: 39, name: 'Clarke' }, { fips: 41, name: 'Clay' }, { fips: 43, name: 'Clayton' }, { fips: 45, name: 'Clinton' }, { fips: 47, name: 'Crawford' }, { fips: 49, name: 'Dallas' }, { fips: 51, name: 'Davis' }, { fips: 53, name: 'Decatur' }, { fips: 55, name: 'Delaware' }, { fips: 57, name: 'Des Moines' }, { fips: 59, name: 'Dickinson' }, { fips: 61, name: 'Dubuque' }, { fips: 63, name: 'Emmet' }, { fips: 65, name: 'Fayette' }, { fips: 67, name: 'Floyd' }, { fips: 69, name: 'Franklin' }, { fips: 71, name: 'Fremont' }, { fips: 73, name: 'Greene' }, { fips: 75, name: 'Grundy' }, { fips: 77, name: 'Guthrie' }, { fips: 79, name: 'Hamilton' }, { fips: 81, name: 'Hancock' }, { fips: 83, name: 'Hardin' }, { fips: 85, name: 'Harrison' }, { fips: 87, name: 'Henry' }, { fips: 89, name: 'Howard' }, { fips: 91, name: 'Humboldt' }, { fips: 93, name: 'Ida' }, { fips: 95, name: 'Iowa' }, { fips: 97, name: 'Jackson' }, { fips: 99, name: 'Jasper' }, { fips: 101, name: 'Jefferson' }, { fips: 103, name: 'Johnson' }, { fips: 105, name: 'Jones' }, { fips: 107, name: 'Keokuk' }, { fips: 109, name: 'Kossuth' }, { fips: 111, name: 'Lee' }, { fips: 113, name: 'Linn' }, { fips: 115, name: 'Louisa' }, { fips: 117, name: 'Lucas' }, { fips: 119, name: 'Lyon' }, { fips: 121, name: 'Madison' }, { fips: 123, name: 'Mahaska' }, { fips: 125, name: 'Marion' }, { fips: 127, name: 'Marshall' }, { fips: 129, name: 'Mills' }, { fips: 131, name: 'Mitchell' }, { fips: 133, name: 'Monona' }, { fips: 135, name: 'Monroe' }, { fips: 137, name: 'Montgomery' }, { fips: 139, name: 'Muscatine' }, { fips: 141, name: 'Obrien' }, { fips: 143, name: 'Osceola' }, { fips: 145, name: 'Page' }, { fips: 147, name: 'Palo Alto' }, { fips: 149, name: 'Plymouth' }, { fips: 151, name: 'Pocahontas' }, { fips: 153, name: 'Polk' }, { fips: 155, name: 'Pottawattamie' }, { fips: 157, name: 'Poweshiek' }, { fips: 159, name: 'Ringgold' }, { fips: 161, name: 'Sac' }, { fips: 163, name: 'Scott' }, { fips: 165, name: 'Shelby' }, { fips: 167, name: 'Sioux' }, { fips: 169, name: 'Story' }, { fips: 171, name: 'Tama' }, { fips: 173, name: 'Taylor' }, { fips: 175, name: 'Union' }, { fips: 177, name: 'Van Buren' }, { fips: 179, name: 'Wapello' }, { fips: 181, name: 'Warren' }, { fips: 183, name: 'Washington' }, { fips: 185, name: 'Wayne' }, { fips: 187, name: 'Webster' }, { fips: 189, name: 'Winnebago' }, { fips: 191, name: 'Winneshiek' }, { fips: 193, name: 'Woodbury' }, { fips: 195, name: 'Worth' }, { fips: 197, name: 'Wright' }],
        'Kansas': [{ fips: 1, name: 'Allen' }, { fips: 3, name: 'Anderson' }, { fips: 5, name: 'Atchison' }, { fips: 7, name: 'Barber' }, { fips: 9, name: 'Barton' }, { fips: 11, name: 'Bourbon' }, { fips: 13, name: 'Brown' }, { fips: 15, name: 'Butler' }, { fips: 17, name: 'Chase' }, { fips: 19, name: 'Chautauqua' }, { fips: 21, name: 'Cherokee' }, { fips: 23, name: 'Cheyenne' }, { fips: 25, name: 'Clark' }, { fips: 27, name: 'Clay' }, { fips: 29, name: 'Cloud' }, { fips: 31, name: 'Coffey' }, { fips: 33, name: 'Comanche' }, { fips: 35, name: 'Cowley' }, { fips: 37, name: 'Crawford' }, { fips: 39, name: 'Decatur' }, { fips: 41, name: 'Dickinson' }, { fips: 43, name: 'Doniphan' }, { fips: 45, name: 'Douglas' }, { fips: 47, name: 'Edwards' }, { fips: 49, name: 'Elk' }, { fips: 51, name: 'Ellis' }, { fips: 53, name: 'Ellsworth' }, { fips: 55, name: 'Finney' }, { fips: 57, name: 'Ford' }, { fips: 59, name: 'Franklin' }, { fips: 61, name: 'Geary' }, { fips: 63, name: 'Gove' }, { fips: 65, name: 'Graham' }, { fips: 67, name: 'Grant' }, { fips: 69, name: 'Gray' }, { fips: 71, name: 'Greeley' }, { fips: 73, name: 'Greenwood' }, { fips: 75, name: 'Hamilton' }, { fips: 77, name: 'Harper' }, { fips: 79, name: 'Harvey' }, { fips: 81, name: 'Haskell' }, { fips: 83, name: 'Hodgeman' }, { fips: 85, name: 'Jackson' }, { fips: 87, name: 'Jefferson' }, { fips: 89, name: 'Jewell' }, { fips: 91, name: 'Johnson' }, { fips: 93, name: 'Kearny' }, { fips: 95, name: 'Kingman' }, { fips: 97, name: 'Kiowa' }, { fips: 99, name: 'Labette' }, { fips: 101, name: 'Lane' }, { fips: 103, name: 'Leavenworth' }, { fips: 105, name: 'Lincoln' }, { fips: 107, name: 'Linn' }, { fips: 109, name: 'Logan' }, { fips: 111, name: 'Lyon' }, { fips: 113, name: 'McPherson' }, { fips: 115, name: 'Marion' }, { fips: 117, name: 'Marshall' }, { fips: 119, name: 'Meade' }, { fips: 121, name: 'Miami' }, { fips: 123, name: 'Mitchell' }, { fips: 125, name: 'Montgomery' }, { fips: 127, name: 'Morris' }, { fips: 129, name: 'Morton' }, { fips: 131, name: 'Nemaha' }, { fips: 133, name: 'Neosho' }, { fips: 135, name: 'Ness' }, { fips: 137, name: 'Norton' }, { fips: 139, name: 'Osage' }, { fips: 141, name: 'Osborne' }, { fips: 143, name: 'Ottawa' }, { fips: 145, name: 'Pawnee' }, { fips: 147, name: 'Phillips' }, { fips: 149, name: 'Pottawatomie' }, { fips: 151, name: 'Pratt' }, { fips: 153, name: 'Rawlins' }, { fips: 155, name: 'Reno' }, { fips: 157, name: 'Republic' }, { fips: 159, name: 'Rice' }, { fips: 161, name: 'Riley' }, { fips: 163, name: 'Rooks' }, { fips: 165, name: 'Rush' }, { fips: 167, name: 'Russell' }, { fips: 169, name: 'Saline' }, { fips: 171, name: 'Scott' }, { fips: 173, name: 'Sedgwick' }, { fips: 175, name: 'Seward' }, { fips: 177, name: 'Shawnee' }, { fips: 179, name: 'Sheridan' }, { fips: 181, name: 'Sherman' }, { fips: 183, name: 'Smith' }, { fips: 185, name: 'Stafford' }, { fips: 187, name: 'Stanton' }, { fips: 189, name: 'Stevens' }, { fips: 191, name: 'Sumner' }, { fips: 193, name: 'Thomas' }, { fips: 195, name: 'Trego' }, { fips: 197, name: 'Wabaunsee' }, { fips: 199, name: 'Wallace' }, { fips: 201, name: 'Washington' }, { fips: 203, name: 'Wichita' }, { fips: 205, name: 'Wilson' }, { fips: 207, name: 'Woodson' }, { fips: 209, name: 'Wyandotte' }],
        'Kentucky': [{ fips: 1, name: 'Adair' }, { fips: 3, name: 'Allen' }, { fips: 5, name: 'Anderson' }, { fips: 7, name: 'Ballard' }, { fips: 9, name: 'Barren' }, { fips: 11, name: 'Bath' }, { fips: 13, name: 'Bell' }, { fips: 15, name: 'Boone' }, { fips: 17, name: 'Bourbon' }, { fips: 19, name: 'Boyd' }, { fips: 21, name: 'Boyle' }, { fips: 23, name: 'Bracken' }, { fips: 25, name: 'Breathitt' }, { fips: 27, name: 'Breckinridge' }, { fips: 29, name: 'Bullitt' }, { fips: 31, name: 'Butler' }, { fips: 33, name: 'Caldwell' }, { fips: 35, name: 'Calloway' }, { fips: 37, name: 'Campbell' }, { fips: 39, name: 'Carlisle' }, { fips: 41, name: 'Carroll' }, { fips: 43, name: 'Carter' }, { fips: 45, name: 'Casey' }, { fips: 47, name: 'Christian' }, { fips: 49, name: 'Clark' }, { fips: 51, name: 'Clay' }, { fips: 53, name: 'Clinton' }, { fips: 55, name: 'Crittenden' }, { fips: 57, name: 'Cumberland' }, { fips: 59, name: 'Daviess' }, { fips: 61, name: 'Edmonson' }, { fips: 63, name: 'Elliott' }, { fips: 65, name: 'Estill' }, { fips: 67, name: 'Fayette' }, { fips: 69, name: 'Fleming' }, { fips: 71, name: 'Floyd' }, { fips: 73, name: 'Franklin' }, { fips: 75, name: 'Fulton' }, { fips: 77, name: 'Gallatin' }, { fips: 79, name: 'Garrard' }, { fips: 81, name: 'Grant' }, { fips: 83, name: 'Graves' }, { fips: 85, name: 'Grayson' }, { fips: 87, name: 'Green' }, { fips: 89, name: 'Greenup' }, { fips: 91, name: 'Hancock' }, { fips: 93, name: 'Hardin' }, { fips: 95, name: 'Harlan' }, { fips: 97, name: 'Harrison' }, { fips: 99, name: 'Hart' }, { fips: 101, name: 'Henderson' }, { fips: 103, name: 'Henry' }, { fips: 105, name: 'Hickman' }, { fips: 107, name: 'Hopkins' }, { fips: 109, name: 'Jackson' }, { fips: 111, name: 'Jefferson' }, { fips: 113, name: 'Jessamine' }, { fips: 115, name: 'Johnson' }, { fips: 117, name: 'Kenton' }, { fips: 119, name: 'Knott' }, { fips: 121, name: 'Knox' }, { fips: 123, name: 'Larue' }, { fips: 125, name: 'Laurel' }, { fips: 127, name: 'Lawrence' }, { fips: 129, name: 'Lee' }, { fips: 131, name: 'Leslie' }, { fips: 133, name: 'Letcher' }, { fips: 135, name: 'Lewis' }, { fips: 137, name: 'Lincoln' }, { fips: 139, name: 'Livingston' }, { fips: 141, name: 'Logan' }, { fips: 143, name: 'Lyon' }, { fips: 145, name: 'McCracken' }, { fips: 147, name: 'McCreary' }, { fips: 149, name: 'Mclean' }, { fips: 151, name: 'Madison' }, { fips: 153, name: 'Magoffin' }, { fips: 155, name: 'Marion' }, { fips: 157, name: 'Marshall' }, { fips: 159, name: 'Martin' }, { fips: 161, name: 'Mason' }, { fips: 163, name: 'Meade' }, { fips: 165, name: 'Menifee' }, { fips: 167, name: 'Mercer' }, { fips: 169, name: 'Metcalfe' }, { fips: 171, name: 'Monroe' }, { fips: 173, name: 'Montgomery' }, { fips: 175, name: 'Morgan' }, { fips: 177, name: 'Muhlenberg' }, { fips: 179, name: 'Nelson' }, { fips: 181, name: 'Nicholas' }, { fips: 183, name: 'Ohio' }, { fips: 185, name: 'Oldham' }, { fips: 187, name: 'Owen' }, { fips: 189, name: 'Owsley' }, { fips: 191, name: 'Pendleton' }, { fips: 193, name: 'Perry' }, { fips: 195, name: 'Pike' }, { fips: 197, name: 'Powell' }, { fips: 199, name: 'Pulaski' }, { fips: 201, name: 'Robertson' }, { fips: 203, name: 'Rockcastle' }, { fips: 205, name: 'Rowan' }, { fips: 207, name: 'Russell' }, { fips: 209, name: 'Scott' }, { fips: 211, name: 'Shelby' }, { fips: 213, name: 'Simpson' }, { fips: 215, name: 'Spencer' }, { fips: 217, name: 'Taylor' }, { fips: 219, name: 'Todd' }, { fips: 221, name: 'Trigg' }, { fips: 223, name: 'Trimble' }, { fips: 225, name: 'Union' }, { fips: 227, name: 'Warren' }, { fips: 229, name: 'Washington' }, { fips: 231, name: 'Wayne' }, { fips: 233, name: 'Webster' }, { fips: 235, name: 'Whitley' }, { fips: 237, name: 'Wolfe' }, { fips: 239, name: 'Woodford' }],
        'Louisiana': [{ fips: 1, name: 'Acadia' }, { fips: 3, name: 'Allen' }, { fips: 5, name: 'Ascension' }, { fips: 7, name: 'Assumption' }, { fips: 9, name: 'Avoyelles' }, { fips: 11, name: 'Beauregard' }, { fips: 13, name: 'Bienville' }, { fips: 15, name: 'Bossier' }, { fips: 17, name: 'Caddo' }, { fips: 19, name: 'Calcasieu' }, { fips: 21, name: 'Caldwell' }, { fips: 23, name: 'Cameron' }, { fips: 25, name: 'Catahoula' }, { fips: 27, name: 'Claiborne' }, { fips: 29, name: 'Concordia' }, { fips: 31, name: 'De Soto' }, { fips: 33, name: 'East Baton Rouge' }, { fips: 35, name: 'East Carroll' }, { fips: 37, name: 'East Feliciana' }, { fips: 39, name: 'Evangeline' }, { fips: 41, name: 'Franklin' }, { fips: 43, name: 'Grant' }, { fips: 45, name: 'Iberia' }, { fips: 47, name: 'Iberville' }, { fips: 49, name: 'Jackson' }, { fips: 51, name: 'Jefferson' }, { fips: 53, name: 'Jefferson Davis' }, { fips: 55, name: 'Lafayette' }, { fips: 57, name: 'Lafourche' }, { fips: 59, name: 'La Salle' }, { fips: 61, name: 'Lincoln' }, { fips: 63, name: 'Livingston' }, { fips: 65, name: 'Madison' }, { fips: 67, name: 'Morehouse' }, { fips: 69, name: 'Natchitoches' }, { fips: 71, name: 'Orleans' }, { fips: 73, name: 'Ouachita' }, { fips: 75, name: 'Plaquemines' }, { fips: 77, name: 'Pointe Coupee' }, { fips: 79, name: 'Rapides' }, { fips: 81, name: 'Red River' }, { fips: 83, name: 'Richland' }, { fips: 85, name: 'Sabine' }, { fips: 87, name: 'St Bernard' }, { fips: 89, name: 'St Charles' }, { fips: 91, name: 'St Helena' }, { fips: 93, name: 'St James' }, { fips: 95, name: 'St John The Baptist' }, { fips: 97, name: 'St Landry' }, { fips: 99, name: 'St Martin' }, { fips: 101, name: 'St Mary' }, { fips: 103, name: 'St Tammany' }, { fips: 105, name: 'Tangipahoa' }, { fips: 107, name: 'Tensas' }, { fips: 109, name: 'Terrebonne' }, { fips: 111, name: 'Union' }, { fips: 113, name: 'Vermilion' }, { fips: 115, name: 'Vernon' }, { fips: 117, name: 'Washington' }, { fips: 119, name: 'Webster' }, { fips: 121, name: 'West Baton Rouge' }, { fips: 123, name: 'West Carroll' }, { fips: 125, name: 'West Feliciana' }, { fips: 127, name: 'Winn' }],
        'Maine': [{ fips: 1, name: 'Androscoggin' }, { fips: 3, name: 'Aroostook' }, { fips: 5, name: 'Cumberland' }, { fips: 7, name: 'Franklin' }, { fips: 9, name: 'Hancock' }, { fips: 11, name: 'Kennebec' }, { fips: 13, name: 'Knox' }, { fips: 15, name: 'Lincoln' }, { fips: 17, name: 'Oxford' }, { fips: 19, name: 'Penobscot' }, { fips: 21, name: 'Piscataquis' }, { fips: 23, name: 'Sagadahoc' }, { fips: 25, name: 'Somerset' }, { fips: 27, name: 'Waldo' }, { fips: 29, name: 'Washington' }, { fips: 31, name: 'York' }],
        'Maryland': [{ fips: 1, name: 'Allegany' }, { fips: 3, name: 'Anne Arundel' }, { fips: 5, name: 'Baltimore' }, { fips: 9, name: 'Calvert' }, { fips: 11, name: 'Caroline' }, { fips: 13, name: 'Carroll' }, { fips: 15, name: 'Cecil' }, { fips: 17, name: 'Charles' }, { fips: 19, name: 'Dorchester' }, { fips: 21, name: 'Frederick' }, { fips: 23, name: 'Garrett' }, { fips: 25, name: 'Harford' }, { fips: 27, name: 'Howard' }, { fips: 29, name: 'Kent' }, { fips: 31, name: 'Montgomery' }, { fips: 33, name: 'Prince Georges' }, { fips: 35, name: 'Queen Annes' }, { fips: 37, name: 'St Marys' }, { fips: 39, name: 'Somerset' }, { fips: 41, name: 'Talbot' }, { fips: 43, name: 'Washington' }, { fips: 45, name: 'Wicomico' }, { fips: 47, name: 'Worcester' }, { fips: 510, name: 'Baltimore City' }],
        'Massachusetts': [{ fips: 1, name: 'Barnstable' }, { fips: 3, name: 'Berkshire' }, { fips: 5, name: 'Bristol' }, { fips: 7, name: 'Dukes' }, { fips: 9, name: 'Essex' }, { fips: 11, name: 'Franklin' }, { fips: 13, name: 'Hampden' }, { fips: 15, name: 'Hampshire' }, { fips: 17, name: 'Middlesex' }, { fips: 19, name: 'Nantucket' }, { fips: 21, name: 'Norfolk' }, { fips: 23, name: 'Plymouth' }, { fips: 25, name: 'Suffolk' }, { fips: 27, name: 'Worcester' }],
        'Michigan': [{ fips: 1, name: 'Alcona' }, { fips: 3, name: 'Alger' }, { fips: 5, name: 'Allegan' }, { fips: 7, name: 'Alpena' }, { fips: 9, name: 'Antrim' }, { fips: 11, name: 'Arenac' }, { fips: 13, name: 'Baraga' }, { fips: 15, name: 'Barry' }, { fips: 17, name: 'Bay' }, { fips: 19, name: 'Benzie' }, { fips: 21, name: 'Berrien' }, { fips: 23, name: 'Branch' }, { fips: 25, name: 'Calhoun' }, { fips: 27, name: 'Cass' }, { fips: 29, name: 'Charlevoix' }, { fips: 31, name: 'Cheboygan' }, { fips: 33, name: 'Chippewa' }, { fips: 35, name: 'Clare' }, { fips: 37, name: 'Clinton' }, { fips: 39, name: 'Crawford' }, { fips: 41, name: 'Delta' }, { fips: 43, name: 'Dickinson' }, { fips: 45, name: 'Eaton' }, { fips: 47, name: 'Emmet' }, { fips: 49, name: 'Genesee' }, { fips: 51, name: 'Gladwin' }, { fips: 53, name: 'Gogebic' }, { fips: 55, name: 'Grand Traverse' }, { fips: 57, name: 'Gratiot' }, { fips: 59, name: 'Hillsdale' }, { fips: 61, name: 'Houghton' }, { fips: 63, name: 'Huron' }, { fips: 65, name: 'Ingham' }, { fips: 67, name: 'Ionia' }, { fips: 69, name: 'Iosco' }, { fips: 71, name: 'Iron' }, { fips: 73, name: 'Isabella' }, { fips: 75, name: 'Jackson' }, { fips: 77, name: 'Kalamazoo' }, { fips: 79, name: 'Kalkaska' }, { fips: 81, name: 'Kent' }, { fips: 83, name: 'Keweenaw' }, { fips: 85, name: 'Lake' }, { fips: 87, name: 'Lapeer' }, { fips: 89, name: 'Leelanau' }, { fips: 91, name: 'Lenawee' }, { fips: 93, name: 'Livingston' }, { fips: 95, name: 'Luce' }, { fips: 97, name: 'Mackinac' }, { fips: 99, name: 'Macomb' }, { fips: 101, name: 'Manistee' }, { fips: 103, name: 'Marquette' }, { fips: 105, name: 'Mason' }, { fips: 107, name: 'Mecosta' }, { fips: 109, name: 'Menominee' }, { fips: 111, name: 'Midland' }, { fips: 113, name: 'Missaukee' }, { fips: 115, name: 'Monroe' }, { fips: 117, name: 'Montcalm' }, { fips: 119, name: 'Montmorency' }, { fips: 121, name: 'Muskegon' }, { fips: 123, name: 'Newaygo' }, { fips: 125, name: 'Oakland' }, { fips: 127, name: 'Oceana' }, { fips: 129, name: 'Ogemaw' }, { fips: 131, name: 'Ontonagon' }, { fips: 133, name: 'Osceola' }, { fips: 135, name: 'Oscoda' }, { fips: 137, name: 'Otsego' }, { fips: 139, name: 'Ottawa' }, { fips: 141, name: 'Presque Isle' }, { fips: 143, name: 'Roscommon' }, { fips: 145, name: 'Saginaw' }, { fips: 147, name: 'St Clair' }, { fips: 149, name: 'St Joseph' }, { fips: 151, name: 'Sanilac' }, { fips: 153, name: 'Schoolcraft' }, { fips: 155, name: 'Shiawassee' }, { fips: 157, name: 'Tuscola' }, { fips: 159, name: 'Van Buren' }, { fips: 161, name: 'Washtenaw' }, { fips: 163, name: 'Wayne' }, { fips: 165, name: 'Wexford' }],
        'Minnesota': [{ fips: 1, name: 'Aitkin' }, { fips: 3, name: 'Anoka' }, { fips: 5, name: 'Becker' }, { fips: 7, name: 'Beltrami' }, { fips: 9, name: 'Benton' }, { fips: 11, name: 'Big Stone' }, { fips: 13, name: 'Blue Earth' }, { fips: 15, name: 'Brown' }, { fips: 17, name: 'Carlton' }, { fips: 19, name: 'Carver' }, { fips: 21, name: 'Cass' }, { fips: 23, name: 'Chippewa' }, { fips: 25, name: 'Chisago' }, { fips: 27, name: 'Clay' }, { fips: 29, name: 'Clearwater' }, { fips: 31, name: 'Cook' }, { fips: 33, name: 'Cottonwood' }, { fips: 35, name: 'Crow Wing' }, { fips: 37, name: 'Dakota' }, { fips: 39, name: 'Dodge' }, { fips: 41, name: 'Douglas' }, { fips: 43, name: 'Faribault' }, { fips: 45, name: 'Fillmore' }, { fips: 47, name: 'Freeborn' }, { fips: 49, name: 'Goodhue' }, { fips: 51, name: 'Grant' }, { fips: 53, name: 'Hennepin' }, { fips: 55, name: 'Houston' }, { fips: 57, name: 'Hubbard' }, { fips: 59, name: 'Isanti' }, { fips: 61, name: 'Itasca' }, { fips: 63, name: 'Jackson' }, { fips: 65, name: 'Kanabec' }, { fips: 67, name: 'Kandiyohi' }, { fips: 69, name: 'Kittson' }, { fips: 71, name: 'Koochiching' }, { fips: 73, name: 'Lac Qui Parle' }, { fips: 75, name: 'Lake' }, { fips: 77, name: 'Lake of The Woods' }, { fips: 79, name: 'Le Sueur' }, { fips: 81, name: 'Lincoln' }, { fips: 83, name: 'Lyon' }, { fips: 85, name: 'McLeod' }, { fips: 87, name: 'Mahnomen' }, { fips: 89, name: 'Marshall' }, { fips: 91, name: 'Martin' }, { fips: 93, name: 'Meeker' }, { fips: 95, name: 'Mille Lacs' }, { fips: 97, name: 'Morrison' }, { fips: 99, name: 'Mower' }, { fips: 101, name: 'Murray' }, { fips: 103, name: 'Nicollet' }, { fips: 105, name: 'Nobles' }, { fips: 107, name: 'Norman' }, { fips: 109, name: 'Olmsted' }, { fips: 111, name: 'Otter Tail' }, { fips: 113, name: 'Pennington' }, { fips: 115, name: 'Pine' }, { fips: 117, name: 'Pipestone' }, { fips: 119, name: 'Polk' }, { fips: 121, name: 'Pope' }, { fips: 123, name: 'Ramsey' }, { fips: 125, name: 'Red Lake' }, { fips: 127, name: 'Redwood' }, { fips: 129, name: 'Renville' }, { fips: 131, name: 'Rice' }, { fips: 133, name: 'Rock' }, { fips: 135, name: 'Roseau' }, { fips: 137, name: 'St Louis' }, { fips: 139, name: 'Scott' }, { fips: 141, name: 'Sherburne' }, { fips: 143, name: 'Sibley' }, { fips: 145, name: 'Stearns' }, { fips: 147, name: 'Steele' }, { fips: 149, name: 'Stevens' }, { fips: 151, name: 'Swift' }, { fips: 153, name: 'Todd' }, { fips: 155, name: 'Traverse' }, { fips: 157, name: 'Wabasha' }, { fips: 159, name: 'Wadena' }, { fips: 161, name: 'Waseca' }, { fips: 163, name: 'Washington' }, { fips: 165, name: 'Watonwan' }, { fips: 167, name: 'Wilkin' }, { fips: 169, name: 'Winona' }, { fips: 171, name: 'Wright' }, { fips: 173, name: 'Yellow Medicine' }],
        'Mississippi': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Alcorn' }, { fips: 5, name: 'Amite' }, { fips: 7, name: 'Attala' }, { fips: 9, name: 'Benton' }, { fips: 11, name: 'Bolivar' }, { fips: 13, name: 'Calhoun' }, { fips: 15, name: 'Carroll' }, { fips: 17, name: 'Chickasaw' }, { fips: 19, name: 'Choctaw' }, { fips: 21, name: 'Claiborne' }, { fips: 23, name: 'Clarke' }, { fips: 25, name: 'Clay' }, { fips: 27, name: 'Coahoma' }, { fips: 29, name: 'Copiah' }, { fips: 31, name: 'Covington' }, { fips: 33, name: 'De Soto' }, { fips: 35, name: 'Forrest' }, { fips: 37, name: 'Franklin' }, { fips: 39, name: 'George' }, { fips: 41, name: 'Greene' }, { fips: 43, name: 'Grenada' }, { fips: 45, name: 'Hancock' }, { fips: 47, name: 'Harrison' }, { fips: 49, name: 'Hinds' }, { fips: 51, name: 'Holmes' }, { fips: 53, name: 'Humphreys' }, { fips: 55, name: 'Issaquena' }, { fips: 57, name: 'Itawamba' }, { fips: 59, name: 'Jackson' }, { fips: 61, name: 'Jasper' }, { fips: 63, name: 'Jefferson' }, { fips: 65, name: 'Jefferson Davis' }, { fips: 67, name: 'Jones' }, { fips: 69, name: 'Kemper' }, { fips: 71, name: 'Lafayette' }, { fips: 73, name: 'Lamar' }, { fips: 75, name: 'Lauderdale' }, { fips: 77, name: 'Lawrence' }, { fips: 79, name: 'Leake' }, { fips: 81, name: 'Lee' }, { fips: 83, name: 'Leflore' }, { fips: 85, name: 'Lincoln' }, { fips: 87, name: 'Lowndes' }, { fips: 89, name: 'Madison' }, { fips: 91, name: 'Marion' }, { fips: 93, name: 'Marshall' }, { fips: 95, name: 'Monroe' }, { fips: 97, name: 'Montgomery' }, { fips: 99, name: 'Neshoba' }, { fips: 101, name: 'Newton' }, { fips: 103, name: 'Noxubee' }, { fips: 105, name: 'Oktibbeha' }, { fips: 107, name: 'Panola' }, { fips: 109, name: 'Pearl River' }, { fips: 111, name: 'Perry' }, { fips: 113, name: 'Pike' }, { fips: 115, name: 'Pontotoc' }, { fips: 117, name: 'Prentiss' }, { fips: 119, name: 'Quitman' }, { fips: 121, name: 'Rankin' }, { fips: 123, name: 'Scott' }, { fips: 125, name: 'Sharkey' }, { fips: 127, name: 'Simpson' }, { fips: 129, name: 'Smith' }, { fips: 131, name: 'Stone' }, { fips: 133, name: 'Sunflower' }, { fips: 135, name: 'Tallahatchie' }, { fips: 137, name: 'Tate' }, { fips: 139, name: 'Tippah' }, { fips: 141, name: 'Tishomingo' }, { fips: 143, name: 'Tunica' }, { fips: 145, name: 'Union' }, { fips: 147, name: 'Walthall' }, { fips: 149, name: 'Warren' }, { fips: 151, name: 'Washington' }, { fips: 153, name: 'Wayne' }, { fips: 155, name: 'Webster' }, { fips: 157, name: 'Wilkinson' }, { fips: 159, name: 'Winston' }, { fips: 161, name: 'Yalobusha' }, { fips: 163, name: 'Yazoo' }],
        'Missouri': [{ fips: 1, name: 'Adair' }, { fips: 3, name: 'Andrew' }, { fips: 5, name: 'Atchison' }, { fips: 7, name: 'Audrain' }, { fips: 9, name: 'Barry' }, { fips: 11, name: 'Barton' }, { fips: 13, name: 'Bates' }, { fips: 15, name: 'Benton' }, { fips: 17, name: 'Bollinger' }, { fips: 19, name: 'Boone' }, { fips: 21, name: 'Buchanan' }, { fips: 23, name: 'Butler' }, { fips: 25, name: 'Caldwell' }, { fips: 27, name: 'Callaway' }, { fips: 29, name: 'Camden' }, { fips: 31, name: 'Cape Girardeau' }, { fips: 33, name: 'Carroll' }, { fips: 35, name: 'Carter' }, { fips: 37, name: 'Cass' }, { fips: 39, name: 'Cedar' }, { fips: 41, name: 'Chariton' }, { fips: 43, name: 'Christian' }, { fips: 45, name: 'Clark' }, { fips: 47, name: 'Clay' }, { fips: 49, name: 'Clinton' }, { fips: 51, name: 'Cole' }, { fips: 53, name: 'Cooper' }, { fips: 55, name: 'Crawford' }, { fips: 57, name: 'Dade' }, { fips: 59, name: 'Dallas' }, { fips: 61, name: 'Daviess' }, { fips: 63, name: 'Dekalb' }, { fips: 65, name: 'Dent' }, { fips: 67, name: 'Douglas' }, { fips: 69, name: 'Dunklin' }, { fips: 71, name: 'Franklin' }, { fips: 73, name: 'Gasconade' }, { fips: 75, name: 'Gentry' }, { fips: 77, name: 'Greene' }, { fips: 79, name: 'Grundy' }, { fips: 81, name: 'Harrison' }, { fips: 83, name: 'Henry' }, { fips: 85, name: 'Hickory' }, { fips: 87, name: 'Holt' }, { fips: 89, name: 'Howard' }, { fips: 91, name: 'Howell' }, { fips: 93, name: 'Iron' }, { fips: 95, name: 'Jackson' }, { fips: 97, name: 'Jasper' }, { fips: 99, name: 'Jefferson' }, { fips: 101, name: 'Johnson' }, { fips: 103, name: 'Knox' }, { fips: 105, name: 'Laclede' }, { fips: 107, name: 'Lafayette' }, { fips: 109, name: 'Lawrence' }, { fips: 111, name: 'Lewis' }, { fips: 113, name: 'Lincoln' }, { fips: 115, name: 'Linn' }, { fips: 117, name: 'Livingston' }, { fips: 119, name: 'Mcdonald' }, { fips: 121, name: 'Macon' }, { fips: 123, name: 'Madison' }, { fips: 125, name: 'Maries' }, { fips: 127, name: 'Marion' }, { fips: 129, name: 'Mercer' }, { fips: 131, name: 'Miller' }, { fips: 133, name: 'Mississippi' }, { fips: 135, name: 'Moniteau' }, { fips: 137, name: 'Monroe' }, { fips: 139, name: 'Montgomery' }, { fips: 141, name: 'Morgan' }, { fips: 143, name: 'New Madrid' }, { fips: 145, name: 'Newton' }, { fips: 147, name: 'Nodaway' }, { fips: 149, name: 'Oregon' }, { fips: 151, name: 'Osage' }, { fips: 153, name: 'Ozark' }, { fips: 155, name: 'Pemiscot' }, { fips: 157, name: 'Perry' }, { fips: 159, name: 'Pettis' }, { fips: 161, name: 'Phelps' }, { fips: 163, name: 'Pike' }, { fips: 165, name: 'Platte' }, { fips: 167, name: 'Polk' }, { fips: 169, name: 'Pulaski' }, { fips: 171, name: 'Putnam' }, { fips: 173, name: 'Ralls' }, { fips: 175, name: 'Randolph' }, { fips: 177, name: 'Ray' }, { fips: 179, name: 'Reynolds' }, { fips: 181, name: 'Ripley' }, { fips: 183, name: 'St Charles' }, { fips: 185, name: 'St Clair' }, { fips: 186, name: 'Ste Genevieve' }, { fips: 187, name: 'St Francois' }, { fips: 189, name: 'St Louis' }, { fips: 195, name: 'Saline' }, { fips: 197, name: 'Schuyler' }, { fips: 199, name: 'Scotland' }, { fips: 201, name: 'Scott' }, { fips: 203, name: 'Shannon' }, { fips: 205, name: 'Shelby' }, { fips: 207, name: 'Stoddard' }, { fips: 209, name: 'Stone' }, { fips: 211, name: 'Sullivan' }, { fips: 213, name: 'Taney' }, { fips: 215, name: 'Texas' }, { fips: 217, name: 'Vernon' }, { fips: 219, name: 'Warren' }, { fips: 221, name: 'Washington' }, { fips: 223, name: 'Wayne' }, { fips: 225, name: 'Webster' }, { fips: 227, name: 'Worth' }, { fips: 229, name: 'Wright' }, { fips: 510, name: 'St Louis City' }],
        'Montana': [{ fips: 1, name: 'Beaverhead' }, { fips: 3, name: 'Big Horn' }, { fips: 5, name: 'Blaine' }, { fips: 7, name: 'Broadwater' }, { fips: 9, name: 'Carbon' }, { fips: 11, name: 'Carter' }, { fips: 13, name: 'Cascade' }, { fips: 15, name: 'Chouteau' }, { fips: 17, name: 'Custer' }, { fips: 19, name: 'Daniels' }, { fips: 21, name: 'Dawson' }, { fips: 23, name: 'Deer Lodge' }, { fips: 25, name: 'Fallon' }, { fips: 27, name: 'Fergus' }, { fips: 29, name: 'Flathead' }, { fips: 31, name: 'Gallatin' }, { fips: 33, name: 'Garfield' }, { fips: 35, name: 'Glacier' }, { fips: 37, name: 'Golden Valley' }, { fips: 39, name: 'Granite' }, { fips: 41, name: 'Hill' }, { fips: 43, name: 'Jefferson' }, { fips: 45, name: 'Judith Basin' }, { fips: 47, name: 'Lake' }, { fips: 49, name: 'Lewis and Clark' }, { fips: 51, name: 'Liberty' }, { fips: 53, name: 'Lincoln' }, { fips: 55, name: 'McCone' }, { fips: 57, name: 'Madison' }, { fips: 59, name: 'Meagher' }, { fips: 61, name: 'Mineral' }, { fips: 63, name: 'Missoula' }, { fips: 65, name: 'Musselshell' }, { fips: 67, name: 'Park' }, { fips: 69, name: 'Petroleum' }, { fips: 71, name: 'Phillips' }, { fips: 73, name: 'Pondera' }, { fips: 75, name: 'Powder River' }, { fips: 77, name: 'Powell' }, { fips: 79, name: 'Prairie' }, { fips: 81, name: 'Ravalli' }, { fips: 83, name: 'Richland' }, { fips: 85, name: 'Roosevelt' }, { fips: 87, name: 'Rosebud' }, { fips: 89, name: 'Sanders' }, { fips: 91, name: 'Sheridan' }, { fips: 93, name: 'Silver Bow' }, { fips: 95, name: 'Stillwater' }, { fips: 97, name: 'Sweet Grass' }, { fips: 99, name: 'Teton' }, { fips: 101, name: 'Toole' }, { fips: 103, name: 'Treasure' }, { fips: 105, name: 'Valley' }, { fips: 107, name: 'Wheatland' }, { fips: 109, name: 'Wibaux' }, { fips: 111, name: 'Yellowstone' }],
        'Nebraska': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Antelope' }, { fips: 5, name: 'Arthur' }, { fips: 7, name: 'Banner' }, { fips: 9, name: 'Blaine' }, { fips: 11, name: 'Boone' }, { fips: 13, name: 'Box Butte' }, { fips: 15, name: 'Boyd' }, { fips: 17, name: 'Brown' }, { fips: 19, name: 'Buffalo' }, { fips: 21, name: 'Burt' }, { fips: 23, name: 'Butler' }, { fips: 25, name: 'Cass' }, { fips: 27, name: 'Cedar' }, { fips: 29, name: 'Chase' }, { fips: 31, name: 'Cherry' }, { fips: 33, name: 'Cheyenne' }, { fips: 35, name: 'Clay' }, { fips: 37, name: 'Colfax' }, { fips: 39, name: 'Cuming' }, { fips: 41, name: 'Custer' }, { fips: 43, name: 'Dakota' }, { fips: 45, name: 'Dawes' }, { fips: 47, name: 'Dawson' }, { fips: 49, name: 'Deuel' }, { fips: 51, name: 'Dixon' }, { fips: 53, name: 'Dodge' }, { fips: 55, name: 'Douglas' }, { fips: 57, name: 'Dundy' }, { fips: 59, name: 'Fillmore' }, { fips: 61, name: 'Franklin' }, { fips: 63, name: 'Frontier' }, { fips: 65, name: 'Furnas' }, { fips: 67, name: 'Gage' }, { fips: 69, name: 'Garden' }, { fips: 71, name: 'Garfield' }, { fips: 73, name: 'Gosper' }, { fips: 75, name: 'Grant' }, { fips: 77, name: 'Greeley' }, { fips: 79, name: 'Hall' }, { fips: 81, name: 'Hamilton' }, { fips: 83, name: 'Harlan' }, { fips: 85, name: 'Hayes' }, { fips: 87, name: 'Hitchcock' }, { fips: 89, name: 'Holt' }, { fips: 91, name: 'Hooker' }, { fips: 93, name: 'Howard' }, { fips: 95, name: 'Jefferson' }, { fips: 97, name: 'Johnson' }, { fips: 99, name: 'Kearney' }, { fips: 101, name: 'Keith' }, { fips: 103, name: 'Keya Paha' }, { fips: 105, name: 'Kimball' }, { fips: 107, name: 'Knox' }, { fips: 109, name: 'Lancaster' }, { fips: 111, name: 'Lincoln' }, { fips: 113, name: 'Logan' }, { fips: 115, name: 'Loup' }, { fips: 117, name: 'McPherson' }, { fips: 119, name: 'Madison' }, { fips: 121, name: 'Merrick' }, { fips: 123, name: 'Morrill' }, { fips: 125, name: 'Nance' }, { fips: 127, name: 'Nemaha' }, { fips: 129, name: 'Nuckolls' }, { fips: 131, name: 'Otoe' }, { fips: 133, name: 'Pawnee' }, { fips: 135, name: 'Perkins' }, { fips: 137, name: 'Phelps' }, { fips: 139, name: 'Pierce' }, { fips: 141, name: 'Platte' }, { fips: 143, name: 'Polk' }, { fips: 145, name: 'Red Willow' }, { fips: 147, name: 'Richardson' }, { fips: 149, name: 'Rock' }, { fips: 151, name: 'Saline' }, { fips: 153, name: 'Sarpy' }, { fips: 155, name: 'Saunders' }, { fips: 157, name: 'Scotts Bluff' }, { fips: 159, name: 'Seward' }, { fips: 161, name: 'Sheridan' }, { fips: 163, name: 'Sherman' }, { fips: 165, name: 'Sioux' }, { fips: 167, name: 'Stanton' }, { fips: 169, name: 'Thayer' }, { fips: 171, name: 'Thomas' }, { fips: 173, name: 'Thurston' }, { fips: 175, name: 'Valley' }, { fips: 177, name: 'Washington' }, { fips: 179, name: 'Wayne' }, { fips: 181, name: 'Webster' }, { fips: 183, name: 'Wheeler' }, { fips: 185, name: 'York' }],
        'Nevada': [{ fips: 1, name: 'Churchill' }, { fips: 3, name: 'Clark' }, { fips: 5, name: 'Douglas' }, { fips: 7, name: 'Elko' }, { fips: 9, name: 'Esmeralda' }, { fips: 11, name: 'Eureka' }, { fips: 13, name: 'Humboldt' }, { fips: 15, name: 'Lander' }, { fips: 17, name: 'Lincoln' }, { fips: 19, name: 'Lyon' }, { fips: 21, name: 'Mineral' }, { fips: 23, name: 'Nye' }, { fips: 27, name: 'Pershing' }, { fips: 29, name: 'Storey' }, { fips: 31, name: 'Washoe' }, { fips: 33, name: 'White Pine' }, { fips: 510, name: 'Carson City' }],
        'New Hampshire': [{ fips: 1, name: 'Belknap' }, { fips: 3, name: 'Carroll' }, { fips: 5, name: 'Cheshire' }, { fips: 7, name: 'Coos' }, { fips: 9, name: 'Grafton' }, { fips: 11, name: 'Hillsborough' }, { fips: 13, name: 'Merrimack' }, { fips: 15, name: 'Rockingham' }, { fips: 17, name: 'Strafford' }, { fips: 19, name: 'Sullivan' }],
        'New Jersey': [{ fips: 1, name: 'Atlantic' }, { fips: 3, name: 'Bergen' }, { fips: 5, name: 'Burlington' }, { fips: 7, name: 'Camden' }, { fips: 9, name: 'Cape May' }, { fips: 11, name: 'Cumberland' }, { fips: 13, name: 'Essex' }, { fips: 15, name: 'Gloucester' }, { fips: 17, name: 'Hudson' }, { fips: 19, name: 'Hunterdon' }, { fips: 21, name: 'Mercer' }, { fips: 23, name: 'Middlesex' }, { fips: 25, name: 'Monmouth' }, { fips: 27, name: 'Morris' }, { fips: 29, name: 'Ocean' }, { fips: 31, name: 'Passaic' }, { fips: 33, name: 'Salem' }, { fips: 35, name: 'Somerset' }, { fips: 37, name: 'Sussex' }, { fips: 39, name: 'Union' }, { fips: 41, name: 'Warren' }],
        'New Mexico': [{ fips: 1, name: 'Bernalillo' }, { fips: 3, name: 'Catron' }, { fips: 5, name: 'Chaves' }, { fips: 6, name: 'Cibola' }, { fips: 7, name: 'Colfax' }, { fips: 9, name: 'Curry' }, { fips: 11, name: 'De Baca' }, { fips: 13, name: 'Dona Ana' }, { fips: 15, name: 'Eddy' }, { fips: 17, name: 'Grant' }, { fips: 19, name: 'Guadalupe' }, { fips: 21, name: 'Harding' }, { fips: 23, name: 'Hidalgo' }, { fips: 25, name: 'Lea' }, { fips: 27, name: 'Lincoln' }, { fips: 28, name: 'Los Alamos' }, { fips: 29, name: 'Luna' }, { fips: 31, name: 'Mckinley' }, { fips: 33, name: 'Mora' }, { fips: 35, name: 'Otero' }, { fips: 37, name: 'Quay' }, { fips: 39, name: 'Rio Arriba' }, { fips: 41, name: 'Roosevelt' }, { fips: 43, name: 'Sandoval' }, { fips: 45, name: 'San Juan' }, { fips: 47, name: 'San Miguel' }, { fips: 49, name: 'Santa Fe' }, { fips: 51, name: 'Sierra' }, { fips: 53, name: 'Socorro' }, { fips: 55, name: 'Taos' }, { fips: 57, name: 'Torrance' }, { fips: 59, name: 'Union' }, { fips: 61, name: 'Valencia' }],
        'New York': [{ fips: 1, name: 'Albany' }, { fips: 3, name: 'Allegany' }, { fips: 5, name: 'Bronx' }, { fips: 7, name: 'Broome' }, { fips: 9, name: 'Cattaraugus' }, { fips: 11, name: 'Cayuga' }, { fips: 13, name: 'Chautauqua' }, { fips: 15, name: 'Chemung' }, { fips: 17, name: 'Chenango' }, { fips: 19, name: 'Clinton' }, { fips: 21, name: 'Columbia' }, { fips: 23, name: 'Cortland' }, { fips: 25, name: 'Delaware' }, { fips: 27, name: 'Dutchess' }, { fips: 29, name: 'Erie' }, { fips: 31, name: 'Essex' }, { fips: 33, name: 'Franklin' }, { fips: 35, name: 'Fulton' }, { fips: 37, name: 'Genesee' }, { fips: 39, name: 'Greene' }, { fips: 41, name: 'Hamilton' }, { fips: 43, name: 'Herkimer' }, { fips: 45, name: 'Jefferson' }, { fips: 47, name: 'Kings' }, { fips: 49, name: 'Lewis' }, { fips: 51, name: 'Livingston' }, { fips: 53, name: 'Madison' }, { fips: 55, name: 'Monroe' }, { fips: 57, name: 'Montgomery' }, { fips: 59, name: 'Nassau' }, { fips: 61, name: 'New York' }, { fips: 63, name: 'Niagara' }, { fips: 65, name: 'Oneida' }, { fips: 67, name: 'Onondaga' }, { fips: 69, name: 'Ontario' }, { fips: 71, name: 'Orange' }, { fips: 73, name: 'Orleans' }, { fips: 75, name: 'Oswego' }, { fips: 77, name: 'Otsego' }, { fips: 79, name: 'Putnam' }, { fips: 81, name: 'Queens' }, { fips: 83, name: 'Rensselaer' }, { fips: 85, name: 'Richmond' }, { fips: 87, name: 'Rockland' }, { fips: 89, name: 'St Lawrence' }, { fips: 91, name: 'Saratoga' }, { fips: 93, name: 'Schenectady' }, { fips: 95, name: 'Schoharie' }, { fips: 97, name: 'Schuyler' }, { fips: 99, name: 'Seneca' }, { fips: 101, name: 'Steuben' }, { fips: 103, name: 'Suffolk' }, { fips: 105, name: 'Sullivan' }, { fips: 107, name: 'Tioga' }, { fips: 109, name: 'Tompkins' }, { fips: 111, name: 'Ulster' }, { fips: 113, name: 'Warren' }, { fips: 115, name: 'Washington' }, { fips: 117, name: 'Wayne' }, { fips: 119, name: 'Westchester' }, { fips: 121, name: 'Wyoming' }, { fips: 123, name: 'Yates' }],
        'North Carolina': [{ fips: 1, name: 'Alamance' }, { fips: 3, name: 'Alexander' }, { fips: 5, name: 'Alleghany' }, { fips: 7, name: 'Anson' }, { fips: 9, name: 'Ashe' }, { fips: 11, name: 'Avery' }, { fips: 13, name: 'Beaufort' }, { fips: 15, name: 'Bertie' }, { fips: 17, name: 'Bladen' }, { fips: 19, name: 'Brunswick' }, { fips: 21, name: 'Buncombe' }, { fips: 23, name: 'Burke' }, { fips: 25, name: 'Cabarrus' }, { fips: 27, name: 'Caldwell' }, { fips: 29, name: 'Camden' }, { fips: 31, name: 'Carteret' }, { fips: 33, name: 'Caswell' }, { fips: 35, name: 'Catawba' }, { fips: 37, name: 'Chatham' }, { fips: 39, name: 'Cherokee' }, { fips: 41, name: 'Chowan' }, { fips: 43, name: 'Clay' }, { fips: 45, name: 'Cleveland' }, { fips: 47, name: 'Columbus' }, { fips: 49, name: 'Craven' }, { fips: 51, name: 'Cumberland' }, { fips: 53, name: 'Currituck' }, { fips: 55, name: 'Dare' }, { fips: 57, name: 'Davidson' }, { fips: 59, name: 'Davie' }, { fips: 61, name: 'Duplin' }, { fips: 63, name: 'Durham' }, { fips: 65, name: 'Edgecombe' }, { fips: 67, name: 'Forsyth' }, { fips: 69, name: 'Franklin' }, { fips: 71, name: 'Gaston' }, { fips: 73, name: 'Gates' }, { fips: 75, name: 'Graham' }, { fips: 77, name: 'Granville' }, { fips: 79, name: 'Greene' }, { fips: 81, name: 'Guilford' }, { fips: 83, name: 'Halifax' }, { fips: 85, name: 'Harnett' }, { fips: 87, name: 'Haywood' }, { fips: 89, name: 'Henderson' }, { fips: 91, name: 'Hertford' }, { fips: 93, name: 'Hoke' }, { fips: 95, name: 'Hyde' }, { fips: 97, name: 'Iredell' }, { fips: 99, name: 'Jackson' }, { fips: 101, name: 'Johnston' }, { fips: 103, name: 'Jones' }, { fips: 105, name: 'Lee' }, { fips: 107, name: 'Lenoir' }, { fips: 109, name: 'Lincoln' }, { fips: 111, name: 'McDowell' }, { fips: 113, name: 'Macon' }, { fips: 115, name: 'Madison' }, { fips: 117, name: 'Martin' }, { fips: 119, name: 'Mecklenburg' }, { fips: 121, name: 'Mitchell' }, { fips: 123, name: 'Montgomery' }, { fips: 125, name: 'Moore' }, { fips: 127, name: 'Nash' }, { fips: 129, name: 'New Hanover' }, { fips: 131, name: 'Northampton' }, { fips: 133, name: 'Onslow' }, { fips: 135, name: 'Orange' }, { fips: 137, name: 'Pamlico' }, { fips: 139, name: 'Pasquotank' }, { fips: 141, name: 'Pender' }, { fips: 143, name: 'Perquimans' }, { fips: 145, name: 'Person' }, { fips: 147, name: 'Pitt' }, { fips: 149, name: 'Polk' }, { fips: 151, name: 'Randolph' }, { fips: 153, name: 'Richmond' }, { fips: 155, name: 'Robeson' }, { fips: 157, name: 'Rockingham' }, { fips: 159, name: 'Rowan' }, { fips: 161, name: 'Rutherford' }, { fips: 163, name: 'Sampson' }, { fips: 165, name: 'Scotland' }, { fips: 167, name: 'Stanly' }, { fips: 169, name: 'Stokes' }, { fips: 171, name: 'Surry' }, { fips: 173, name: 'Swain' }, { fips: 175, name: 'Transylvania' }, { fips: 177, name: 'Tyrrell' }, { fips: 179, name: 'Union' }, { fips: 181, name: 'Vance' }, { fips: 183, name: 'Wake' }, { fips: 185, name: 'Warren' }, { fips: 187, name: 'Washington' }, { fips: 189, name: 'Watauga' }, { fips: 191, name: 'Wayne' }, { fips: 193, name: 'Wilkes' }, { fips: 195, name: 'Wilson' }, { fips: 197, name: 'Yadkin' }, { fips: 199, name: 'Yancey' }],
        'North Dakota': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Barnes' }, { fips: 5, name: 'Benson' }, { fips: 7, name: 'Billings' }, { fips: 9, name: 'Bottineau' }, { fips: 11, name: 'Bowman' }, { fips: 13, name: 'Burke' }, { fips: 15, name: 'Burleigh' }, { fips: 17, name: 'Cass' }, { fips: 19, name: 'Cavalier' }, { fips: 21, name: 'Dickey' }, { fips: 23, name: 'Divide' }, { fips: 25, name: 'Dunn' }, { fips: 27, name: 'Eddy' }, { fips: 29, name: 'Emmons' }, { fips: 31, name: 'Foster' }, { fips: 33, name: 'Golden Valley' }, { fips: 35, name: 'Grand Forks' }, { fips: 37, name: 'Grant' }, { fips: 39, name: 'Griggs' }, { fips: 41, name: 'Hettinger' }, { fips: 43, name: 'Kidder' }, { fips: 45, name: 'Lamoure' }, { fips: 47, name: 'Logan' }, { fips: 49, name: 'McHenry' }, { fips: 51, name: 'McIntosh' }, { fips: 53, name: 'Mckenzie' }, { fips: 55, name: 'Mclean' }, { fips: 57, name: 'Mercer' }, { fips: 59, name: 'Morton' }, { fips: 61, name: 'Mountrail' }, { fips: 63, name: 'Nelson' }, { fips: 65, name: 'Oliver' }, { fips: 67, name: 'Pembina' }, { fips: 69, name: 'Pierce' }, { fips: 71, name: 'Ramsey' }, { fips: 73, name: 'Ransom' }, { fips: 75, name: 'Renville' }, { fips: 77, name: 'Richland' }, { fips: 79, name: 'Rolette' }, { fips: 81, name: 'Sargent' }, { fips: 83, name: 'Sheridan' }, { fips: 85, name: 'Sioux' }, { fips: 87, name: 'Slope' }, { fips: 89, name: 'Stark' }, { fips: 91, name: 'Steele' }, { fips: 93, name: 'Stutsman' }, { fips: 95, name: 'Towner' }, { fips: 97, name: 'Traill' }, { fips: 99, name: 'Walsh' }, { fips: 101, name: 'Ward' }, { fips: 103, name: 'Wells' }, { fips: 105, name: 'Williams' }],
        'Ohio': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Allen' }, { fips: 5, name: 'Ashland' }, { fips: 7, name: 'Ashtabula' }, { fips: 9, name: 'Athens' }, { fips: 11, name: 'Auglaize' }, { fips: 13, name: 'Belmont' }, { fips: 15, name: 'Brown' }, { fips: 17, name: 'Butler' }, { fips: 19, name: 'Carroll' }, { fips: 21, name: 'Champaign' }, { fips: 23, name: 'Clark' }, { fips: 25, name: 'Clermont' }, { fips: 27, name: 'Clinton' }, { fips: 29, name: 'Columbiana' }, { fips: 31, name: 'Coshocton' }, { fips: 33, name: 'Crawford' }, { fips: 35, name: 'Cuyahoga' }, { fips: 37, name: 'Darke' }, { fips: 39, name: 'Defiance' }, { fips: 41, name: 'Delaware' }, { fips: 43, name: 'Erie' }, { fips: 45, name: 'Fairfield' }, { fips: 47, name: 'Fayette' }, { fips: 49, name: 'Franklin' }, { fips: 51, name: 'Fulton' }, { fips: 53, name: 'Gallia' }, { fips: 55, name: 'Geauga' }, { fips: 57, name: 'Greene' }, { fips: 59, name: 'Guernsey' }, { fips: 61, name: 'Hamilton' }, { fips: 63, name: 'Hancock' }, { fips: 65, name: 'Hardin' }, { fips: 67, name: 'Harrison' }, { fips: 69, name: 'Henry' }, { fips: 71, name: 'Highland' }, { fips: 73, name: 'Hocking' }, { fips: 75, name: 'Holmes' }, { fips: 77, name: 'Huron' }, { fips: 79, name: 'Jackson' }, { fips: 81, name: 'Jefferson' }, { fips: 83, name: 'Knox' }, { fips: 85, name: 'Lake' }, { fips: 87, name: 'Lawrence' }, { fips: 89, name: 'Licking' }, { fips: 91, name: 'Logan' }, { fips: 93, name: 'Lorain' }, { fips: 95, name: 'Lucas' }, { fips: 97, name: 'Madison' }, { fips: 99, name: 'Mahoning' }, { fips: 101, name: 'Marion' }, { fips: 103, name: 'Medina' }, { fips: 105, name: 'Meigs' }, { fips: 107, name: 'Mercer' }, { fips: 109, name: 'Miami' }, { fips: 111, name: 'Monroe' }, { fips: 113, name: 'Montgomery' }, { fips: 115, name: 'Morgan' }, { fips: 117, name: 'Morrow' }, { fips: 119, name: 'Muskingum' }, { fips: 121, name: 'Noble' }, { fips: 123, name: 'Ottawa' }, { fips: 125, name: 'Paulding' }, { fips: 127, name: 'Perry' }, { fips: 129, name: 'Pickaway' }, { fips: 131, name: 'Pike' }, { fips: 133, name: 'Portage' }, { fips: 135, name: 'Preble' }, { fips: 137, name: 'Putnam' }, { fips: 139, name: 'Richland' }, { fips: 141, name: 'Ross' }, { fips: 143, name: 'Sandusky' }, { fips: 145, name: 'Scioto' }, { fips: 147, name: 'Seneca' }, { fips: 149, name: 'Shelby' }, { fips: 151, name: 'Stark' }, { fips: 153, name: 'Summit' }, { fips: 155, name: 'Trumbull' }, { fips: 157, name: 'Tuscarawas' }, { fips: 159, name: 'Union' }, { fips: 161, name: 'Van Wert' }, { fips: 163, name: 'Vinton' }, { fips: 165, name: 'Warren' }, { fips: 167, name: 'Washington' }, { fips: 169, name: 'Wayne' }, { fips: 171, name: 'Williams' }, { fips: 173, name: 'Wood' }, { fips: 175, name: 'Wyandot' }],
        'Oklahoma': [{ fips: 1, name: 'Adair' }, { fips: 3, name: 'Alfalfa' }, { fips: 5, name: 'Atoka' }, { fips: 7, name: 'Beaver' }, { fips: 9, name: 'Beckham' }, { fips: 11, name: 'Blaine' }, { fips: 13, name: 'Bryan' }, { fips: 15, name: 'Caddo' }, { fips: 17, name: 'Canadian' }, { fips: 19, name: 'Carter' }, { fips: 21, name: 'Cherokee' }, { fips: 23, name: 'Choctaw' }, { fips: 25, name: 'Cimarron' }, { fips: 27, name: 'Cleveland' }, { fips: 29, name: 'Coal' }, { fips: 31, name: 'Comanche' }, { fips: 33, name: 'Cotton' }, { fips: 35, name: 'Craig' }, { fips: 37, name: 'Creek' }, { fips: 39, name: 'Custer' }, { fips: 41, name: 'Delaware' }, { fips: 43, name: 'Dewey' }, { fips: 45, name: 'Ellis' }, { fips: 47, name: 'Garfield' }, { fips: 49, name: 'Garvin' }, { fips: 51, name: 'Grady' }, { fips: 53, name: 'Grant' }, { fips: 55, name: 'Greer' }, { fips: 57, name: 'Harmon' }, { fips: 59, name: 'Harper' }, { fips: 61, name: 'Haskell' }, { fips: 63, name: 'Hughes' }, { fips: 65, name: 'Jackson' }, { fips: 67, name: 'Jefferson' }, { fips: 69, name: 'Johnston' }, { fips: 71, name: 'Kay' }, { fips: 73, name: 'Kingfisher' }, { fips: 75, name: 'Kiowa' }, { fips: 77, name: 'Latimer' }, { fips: 79, name: 'Le Flore' }, { fips: 81, name: 'Lincoln' }, { fips: 83, name: 'Logan' }, { fips: 85, name: 'Love' }, { fips: 87, name: 'Mcclain' }, { fips: 89, name: 'McCurtain' }, { fips: 91, name: 'McIntosh' }, { fips: 93, name: 'Major' }, { fips: 95, name: 'Marshall' }, { fips: 97, name: 'Mayes' }, { fips: 99, name: 'Murray' }, { fips: 101, name: 'Muskogee' }, { fips: 103, name: 'Noble' }, { fips: 105, name: 'Nowata' }, { fips: 107, name: 'Okfuskee' }, { fips: 109, name: 'Oklahoma' }, { fips: 111, name: 'Okmulgee' }, { fips: 113, name: 'Osage' }, { fips: 115, name: 'Ottawa' }, { fips: 117, name: 'Pawnee' }, { fips: 119, name: 'Payne' }, { fips: 121, name: 'Pittsburg' }, { fips: 123, name: 'Pontotoc' }, { fips: 125, name: 'Pottawatomie' }, { fips: 127, name: 'Pushmataha' }, { fips: 129, name: 'Roger Mills' }, { fips: 131, name: 'Rogers' }, { fips: 133, name: 'Seminole' }, { fips: 135, name: 'Sequoyah' }, { fips: 137, name: 'Stephens' }, { fips: 139, name: 'Texas' }, { fips: 141, name: 'Tillman' }, { fips: 143, name: 'Tulsa' }, { fips: 145, name: 'Wagoner' }, { fips: 147, name: 'Washington' }, { fips: 149, name: 'Washita' }, { fips: 151, name: 'Woods' }, { fips: 153, name: 'Woodward' }],
        'Oregon': [{ fips: 1, name: 'Baker' }, { fips: 3, name: 'Benton' }, { fips: 5, name: 'Clackamas' }, { fips: 7, name: 'Clatsop' }, { fips: 9, name: 'Columbia' }, { fips: 11, name: 'Coos' }, { fips: 13, name: 'Crook' }, { fips: 15, name: 'Curry' }, { fips: 17, name: 'Deschutes' }, { fips: 19, name: 'Douglas' }, { fips: 21, name: 'Gilliam' }, { fips: 23, name: 'Grant' }, { fips: 25, name: 'Harney' }, { fips: 27, name: 'Hood River' }, { fips: 29, name: 'Jackson' }, { fips: 31, name: 'Jefferson' }, { fips: 33, name: 'Josephine' }, { fips: 35, name: 'Klamath' }, { fips: 37, name: 'Lake' }, { fips: 39, name: 'Lane' }, { fips: 41, name: 'Lincoln' }, { fips: 43, name: 'Linn' }, { fips: 45, name: 'Malheur' }, { fips: 47, name: 'Marion' }, { fips: 49, name: 'Morrow' }, { fips: 51, name: 'Multnomah' }, { fips: 53, name: 'Polk' }, { fips: 55, name: 'Sherman' }, { fips: 57, name: 'Tillamook' }, { fips: 59, name: 'Umatilla' }, { fips: 61, name: 'Union' }, { fips: 63, name: 'Wallowa' }, { fips: 65, name: 'Wasco' }, { fips: 67, name: 'Washington' }, { fips: 69, name: 'Wheeler' }, { fips: 71, name: 'Yamhill' }],
        'Pennsylvania': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Allegheny' }, { fips: 5, name: 'Armstrong' }, { fips: 7, name: 'Beaver' }, { fips: 9, name: 'Bedford' }, { fips: 11, name: 'Berks' }, { fips: 13, name: 'Blair' }, { fips: 15, name: 'Bradford' }, { fips: 17, name: 'Bucks' }, { fips: 19, name: 'Butler' }, { fips: 21, name: 'Cambria' }, { fips: 23, name: 'Cameron' }, { fips: 25, name: 'Carbon' }, { fips: 27, name: 'Centre' }, { fips: 29, name: 'Chester' }, { fips: 31, name: 'Clarion' }, { fips: 33, name: 'Clearfield' }, { fips: 35, name: 'Clinton' }, { fips: 37, name: 'Columbia' }, { fips: 39, name: 'Crawford' }, { fips: 41, name: 'Cumberland' }, { fips: 43, name: 'Dauphin' }, { fips: 45, name: 'Delaware' }, { fips: 47, name: 'Elk' }, { fips: 49, name: 'Erie' }, { fips: 51, name: 'Fayette' }, { fips: 53, name: 'Forest' }, { fips: 55, name: 'Franklin' }, { fips: 57, name: 'Fulton' }, { fips: 59, name: 'Greene' }, { fips: 61, name: 'Huntingdon' }, { fips: 63, name: 'Indiana' }, { fips: 65, name: 'Jefferson' }, { fips: 67, name: 'Juniata' }, { fips: 69, name: 'Lackawanna' }, { fips: 71, name: 'Lancaster' }, { fips: 73, name: 'Lawrence' }, { fips: 75, name: 'Lebanon' }, { fips: 77, name: 'Lehigh' }, { fips: 79, name: 'Luzerne' }, { fips: 81, name: 'Lycoming' }, { fips: 83, name: 'McKean' }, { fips: 85, name: 'Mercer' }, { fips: 87, name: 'Mifflin' }, { fips: 89, name: 'Monroe' }, { fips: 91, name: 'Montgomery' }, { fips: 93, name: 'Montour' }, { fips: 95, name: 'Northampton' }, { fips: 97, name: 'Northumberland' }, { fips: 99, name: 'Perry' }, { fips: 101, name: 'Philadelphia' }, { fips: 103, name: 'Pike' }, { fips: 105, name: 'Potter' }, { fips: 107, name: 'Schuylkill' }, { fips: 109, name: 'Snyder' }, { fips: 111, name: 'Somerset' }, { fips: 113, name: 'Sullivan' }, { fips: 115, name: 'Susquehanna' }, { fips: 117, name: 'Tioga' }, { fips: 119, name: 'Union' }, { fips: 121, name: 'Venango' }, { fips: 123, name: 'Warren' }, { fips: 125, name: 'Washington' }, { fips: 127, name: 'Wayne' }, { fips: 129, name: 'Westmoreland' }, { fips: 131, name: 'Wyoming' }, { fips: 133, name: 'York' }],
        'Rhode Island': [{ fips: 1, name: 'Bristol' }, { fips: 3, name: 'Kent' }, { fips: 5, name: 'Newport' }, { fips: 7, name: 'Providence' }, { fips: 9, name: 'Washington' }],
        'South Carolina': [{ fips: 1, name: 'Abbeville' }, { fips: 3, name: 'Aiken' }, { fips: 5, name: 'Allendale' }, { fips: 7, name: 'Anderson' }, { fips: 9, name: 'Bamberg' }, { fips: 11, name: 'Barnwell' }, { fips: 13, name: 'Beaufort' }, { fips: 15, name: 'Berkeley' }, { fips: 17, name: 'Calhoun' }, { fips: 19, name: 'Charleston' }, { fips: 21, name: 'Cherokee' }, { fips: 23, name: 'Chester' }, { fips: 25, name: 'Chesterfield' }, { fips: 27, name: 'Clarendon' }, { fips: 29, name: 'Colleton' }, { fips: 31, name: 'Darlington' }, { fips: 33, name: 'Dillon' }, { fips: 35, name: 'Dorchester' }, { fips: 37, name: 'Edgefield' }, { fips: 39, name: 'Fairfield' }, { fips: 41, name: 'Florence' }, { fips: 43, name: 'Georgetown' }, { fips: 45, name: 'Greenville' }, { fips: 47, name: 'Greenwood' }, { fips: 49, name: 'Hampton' }, { fips: 51, name: 'Horry' }, { fips: 53, name: 'Jasper' }, { fips: 55, name: 'Kershaw' }, { fips: 57, name: 'Lancaster' }, { fips: 59, name: 'Laurens' }, { fips: 61, name: 'Lee' }, { fips: 63, name: 'Lexington' }, { fips: 65, name: 'McCormick' }, { fips: 67, name: 'Marion' }, { fips: 69, name: 'Marlboro' }, { fips: 71, name: 'Newberry' }, { fips: 73, name: 'Oconee' }, { fips: 75, name: 'Orangeburg' }, { fips: 77, name: 'Pickens' }, { fips: 79, name: 'Richland' }, { fips: 81, name: 'Saluda' }, { fips: 83, name: 'Spartanburg' }, { fips: 85, name: 'Sumter' }, { fips: 87, name: 'Union' }, { fips: 89, name: 'Williamsburg' }, { fips: 91, name: 'York' }],
        'South Dakota': [{ fips: 3, name: 'Aurora' }, { fips: 5, name: 'Beadle' }, { fips: 7, name: 'Bennett' }, { fips: 9, name: 'Bon Homme' }, { fips: 11, name: 'Brookings' }, { fips: 13, name: 'Brown' }, { fips: 15, name: 'Brule' }, { fips: 17, name: 'Buffalo' }, { fips: 19, name: 'Butte' }, { fips: 21, name: 'Campbell' }, { fips: 23, name: 'Charles Mix' }, { fips: 25, name: 'Clark' }, { fips: 27, name: 'Clay' }, { fips: 29, name: 'Codington' }, { fips: 31, name: 'Corson' }, { fips: 33, name: 'Custer' }, { fips: 35, name: 'Davison' }, { fips: 37, name: 'Day' }, { fips: 39, name: 'Deuel' }, { fips: 41, name: 'Dewey' }, { fips: 43, name: 'Douglas' }, { fips: 45, name: 'Edmunds' }, { fips: 47, name: 'Fall River' }, { fips: 49, name: 'Faulk' }, { fips: 51, name: 'Grant' }, { fips: 53, name: 'Gregory' }, { fips: 55, name: 'Haakon' }, { fips: 57, name: 'Hamlin' }, { fips: 59, name: 'Hand' }, { fips: 61, name: 'Hanson' }, { fips: 63, name: 'Harding' }, { fips: 65, name: 'Hughes' }, { fips: 67, name: 'Hutchinson' }, { fips: 69, name: 'Hyde' }, { fips: 71, name: 'Jackson' }, { fips: 73, name: 'Jerauld' }, { fips: 75, name: 'Jones' }, { fips: 77, name: 'Kingsbury' }, { fips: 79, name: 'Lake' }, { fips: 81, name: 'Lawrence' }, { fips: 83, name: 'Lincoln' }, { fips: 85, name: 'Lyman' }, { fips: 87, name: 'McCook' }, { fips: 89, name: 'McPherson' }, { fips: 91, name: 'Marshall' }, { fips: 93, name: 'Meade' }, { fips: 95, name: 'Mellette' }, { fips: 97, name: 'Miner' }, { fips: 99, name: 'Minnehaha' }, { fips: 101, name: 'Moody' }, { fips: 103, name: 'Pennington' }, { fips: 105, name: 'Perkins' }, { fips: 107, name: 'Potter' }, { fips: 109, name: 'Roberts' }, { fips: 111, name: 'Sanborn' }, { fips: 113, name: 'Shannon' }, { fips: 115, name: 'Spink' }, { fips: 117, name: 'Stanley' }, { fips: 119, name: 'Sully' }, { fips: 121, name: 'Todd' }, { fips: 123, name: 'Tripp' }, { fips: 125, name: 'Turner' }, { fips: 127, name: 'Union' }, { fips: 129, name: 'Walworth' }, { fips: 135, name: 'Yankton' }, { fips: 137, name: 'Ziebach' }],
        'Tennessee': [{ fips: 1, name: 'Anderson' }, { fips: 3, name: 'Bedford' }, { fips: 5, name: 'Benton' }, { fips: 7, name: 'Bledsoe' }, { fips: 9, name: 'Blount' }, { fips: 11, name: 'Bradley' }, { fips: 13, name: 'Campbell' }, { fips: 15, name: 'Cannon' }, { fips: 17, name: 'Carroll' }, { fips: 19, name: 'Carter' }, { fips: 21, name: 'Cheatham' }, { fips: 23, name: 'Chester' }, { fips: 25, name: 'Claiborne' }, { fips: 27, name: 'Clay' }, { fips: 29, name: 'Cocke' }, { fips: 31, name: 'Coffee' }, { fips: 33, name: 'Crockett' }, { fips: 35, name: 'Cumberland' }, { fips: 37, name: 'Davidson' }, { fips: 39, name: 'Decatur' }, { fips: 41, name: 'Dekalb' }, { fips: 43, name: 'Dickson' }, { fips: 45, name: 'Dyer' }, { fips: 47, name: 'Fayette' }, { fips: 49, name: 'Fentress' }, { fips: 51, name: 'Franklin' }, { fips: 53, name: 'Gibson' }, { fips: 55, name: 'Giles' }, { fips: 57, name: 'Grainger' }, { fips: 59, name: 'Greene' }, { fips: 61, name: 'Grundy' }, { fips: 63, name: 'Hamblen' }, { fips: 65, name: 'Hamilton' }, { fips: 67, name: 'Hancock' }, { fips: 69, name: 'Hardeman' }, { fips: 71, name: 'Hardin' }, { fips: 73, name: 'Hawkins' }, { fips: 75, name: 'Haywood' }, { fips: 77, name: 'Henderson' }, { fips: 79, name: 'Henry' }, { fips: 81, name: 'Hickman' }, { fips: 83, name: 'Houston' }, { fips: 85, name: 'Humphreys' }, { fips: 87, name: 'Jackson' }, { fips: 89, name: 'Jefferson' }, { fips: 91, name: 'Johnson' }, { fips: 93, name: 'Knox' }, { fips: 95, name: 'Lake' }, { fips: 97, name: 'Lauderdale' }, { fips: 99, name: 'Lawrence' }, { fips: 101, name: 'Lewis' }, { fips: 103, name: 'Lincoln' }, { fips: 105, name: 'Loudon' }, { fips: 107, name: 'McMinn' }, { fips: 109, name: 'McNairy' }, { fips: 111, name: 'Macon' }, { fips: 113, name: 'Madison' }, { fips: 115, name: 'Marion' }, { fips: 117, name: 'Marshall' }, { fips: 119, name: 'Maury' }, { fips: 121, name: 'Meigs' }, { fips: 123, name: 'Monroe' }, { fips: 125, name: 'Montgomery' }, { fips: 127, name: 'Moore' }, { fips: 129, name: 'Morgan' }, { fips: 131, name: 'Obion' }, { fips: 133, name: 'Overton' }, { fips: 135, name: 'Perry' }, { fips: 137, name: 'Pickett' }, { fips: 139, name: 'Polk' }, { fips: 141, name: 'Putnam' }, { fips: 143, name: 'Rhea' }, { fips: 145, name: 'Roane' }, { fips: 147, name: 'Robertson' }, { fips: 149, name: 'Rutherford' }, { fips: 151, name: 'Scott' }, { fips: 153, name: 'Sequatchie' }, { fips: 155, name: 'Sevier' }, { fips: 157, name: 'Shelby' }, { fips: 159, name: 'Smith' }, { fips: 161, name: 'Stewart' }, { fips: 163, name: 'Sullivan' }, { fips: 165, name: 'Sumner' }, { fips: 167, name: 'Tipton' }, { fips: 169, name: 'Trousdale' }, { fips: 171, name: 'Unicoi' }, { fips: 173, name: 'Union' }, { fips: 175, name: 'Van Buren' }, { fips: 177, name: 'Warren' }, { fips: 179, name: 'Washington' }, { fips: 181, name: 'Wayne' }, { fips: 183, name: 'Weakley' }, { fips: 185, name: 'White' }, { fips: 187, name: 'Williamson' }, { fips: 189, name: 'Wilson' }],
        'Texas': [{ fips: 1, name: 'Anderson' }, { fips: 3, name: 'Andrews' }, { fips: 5, name: 'Angelina' }, { fips: 7, name: 'Aransas' }, { fips: 9, name: 'Archer' }, { fips: 11, name: 'Armstrong' }, { fips: 13, name: 'Atascosa' }, { fips: 15, name: 'Austin' }, { fips: 17, name: 'Bailey' }, { fips: 19, name: 'Bandera' }, { fips: 21, name: 'Bastrop' }, { fips: 23, name: 'Baylor' }, { fips: 25, name: 'Bee' }, { fips: 27, name: 'Bell' }, { fips: 29, name: 'Bexar' }, { fips: 31, name: 'Blanco' }, { fips: 33, name: 'Borden' }, { fips: 35, name: 'Bosque' }, { fips: 37, name: 'Bowie' }, { fips: 39, name: 'Brazoria' }, { fips: 41, name: 'Brazos' }, { fips: 43, name: 'Brewster' }, { fips: 45, name: 'Briscoe' }, { fips: 47, name: 'Brooks' }, { fips: 49, name: 'Brown' }, { fips: 51, name: 'Burleson' }, { fips: 53, name: 'Burnet' }, { fips: 55, name: 'Caldwell' }, { fips: 57, name: 'Calhoun' }, { fips: 59, name: 'Callahan' }, { fips: 61, name: 'Cameron' }, { fips: 63, name: 'Camp' }, { fips: 65, name: 'Carson' }, { fips: 67, name: 'Cass' }, { fips: 69, name: 'Castro' }, { fips: 71, name: 'Chambers' }, { fips: 73, name: 'Cherokee' }, { fips: 75, name: 'Childress' }, { fips: 77, name: 'Clay' }, { fips: 79, name: 'Cochran' }, { fips: 81, name: 'Coke' }, { fips: 83, name: 'Coleman' }, { fips: 85, name: 'Collin' }, { fips: 87, name: 'Collingsworth' }, { fips: 89, name: 'Colorado' }, { fips: 91, name: 'Comal' }, { fips: 93, name: 'Comanche' }, { fips: 95, name: 'Concho' }, { fips: 97, name: 'Cooke' }, { fips: 99, name: 'Coryell' }, { fips: 101, name: 'Cottle' }, { fips: 103, name: 'Crane' }, { fips: 105, name: 'Crockett' }, { fips: 107, name: 'Crosby' }, { fips: 109, name: 'Culberson' }, { fips: 111, name: 'Dallam' }, { fips: 113, name: 'Dallas' }, { fips: 115, name: 'Dawson' }, { fips: 117, name: 'Deaf Smith' }, { fips: 119, name: 'Delta' }, { fips: 121, name: 'Denton' }, { fips: 123, name: 'De Witt' }, { fips: 125, name: 'Dickens' }, { fips: 127, name: 'Dimmit' }, { fips: 129, name: 'Donley' }, { fips: 131, name: 'Duval' }, { fips: 133, name: 'Eastland' }, { fips: 135, name: 'Ector' }, { fips: 137, name: 'Edwards' }, { fips: 139, name: 'Ellis' }, { fips: 141, name: 'El Paso' }, { fips: 143, name: 'Erath' }, { fips: 145, name: 'Falls' }, { fips: 147, name: 'Fannin' }, { fips: 149, name: 'Fayette' }, { fips: 151, name: 'Fisher' }, { fips: 153, name: 'Floyd' }, { fips: 155, name: 'Foard' }, { fips: 157, name: 'Fort Bend' }, { fips: 159, name: 'Franklin' }, { fips: 161, name: 'Freestone' }, { fips: 163, name: 'Frio' }, { fips: 165, name: 'Gaines' }, { fips: 167, name: 'Galveston' }, { fips: 169, name: 'Garza' }, { fips: 171, name: 'Gillespie' }, { fips: 173, name: 'Glasscock' }, { fips: 175, name: 'Goliad' }, { fips: 177, name: 'Gonzales' }, { fips: 179, name: 'Gray' }, { fips: 181, name: 'Grayson' }, { fips: 183, name: 'Gregg' }, { fips: 185, name: 'Grimes' }, { fips: 187, name: 'Guadalupe' }, { fips: 189, name: 'Hale' }, { fips: 191, name: 'Hall' }, { fips: 193, name: 'Hamilton' }, { fips: 195, name: 'Hansford' }, { fips: 197, name: 'Hardeman' }, { fips: 199, name: 'Hardin' }, { fips: 201, name: 'Harris' }, { fips: 203, name: 'Harrison' }, { fips: 205, name: 'Hartley' }, { fips: 207, name: 'Haskell' }, { fips: 209, name: 'Hays' }, { fips: 211, name: 'Hemphill' }, { fips: 213, name: 'Henderson' }, { fips: 215, name: 'Hidalgo' }, { fips: 217, name: 'Hill' }, { fips: 219, name: 'Hockley' }, { fips: 221, name: 'Hood' }, { fips: 223, name: 'Hopkins' }, { fips: 225, name: 'Houston' }, { fips: 227, name: 'Howard' }, { fips: 229, name: 'Hudspeth' }, { fips: 231, name: 'Hunt' }, { fips: 233, name: 'Hutchinson' }, { fips: 235, name: 'Irion' }, { fips: 237, name: 'Jack' }, { fips: 239, name: 'Jackson' }, { fips: 241, name: 'Jasper' }, { fips: 243, name: 'Jeff Davis' }, { fips: 245, name: 'Jefferson' }, { fips: 247, name: 'Jim Hogg' }, { fips: 249, name: 'Jim Wells' }, { fips: 251, name: 'Johnson' }, { fips: 253, name: 'Jones' }, { fips: 255, name: 'Karnes' }, { fips: 257, name: 'Kaufman' }, { fips: 259, name: 'Kendall' }, { fips: 261, name: 'Kenedy' }, { fips: 263, name: 'Kent' }, { fips: 265, name: 'Kerr' }, { fips: 267, name: 'Kimble' }, { fips: 269, name: 'King' }, { fips: 271, name: 'Kinney' }, { fips: 273, name: 'Kleberg' }, { fips: 275, name: 'Knox' }, { fips: 277, name: 'Lamar' }, { fips: 279, name: 'Lamb' }, { fips: 281, name: 'Lampasas' }, { fips: 283, name: 'La Salle' }, { fips: 285, name: 'Lavaca' }, { fips: 287, name: 'Lee' }, { fips: 289, name: 'Leon' }, { fips: 291, name: 'Liberty' }, { fips: 293, name: 'Limestone' }, { fips: 295, name: 'Lipscomb' }, { fips: 297, name: 'Live Oak' }, { fips: 299, name: 'Llano' }, { fips: 301, name: 'Loving' }, { fips: 303, name: 'Lubbock' }, { fips: 305, name: 'Lynn' }, { fips: 307, name: 'McCulloch' }, { fips: 309, name: 'McLennan' }, { fips: 311, name: 'McMullen' }, { fips: 313, name: 'Madison' }, { fips: 315, name: 'Marion' }, { fips: 317, name: 'Martin' }, { fips: 319, name: 'Mason' }, { fips: 321, name: 'Matagorda' }, { fips: 323, name: 'Maverick' }, { fips: 325, name: 'Medina' }, { fips: 327, name: 'Menard' }, { fips: 329, name: 'Midland' }, { fips: 331, name: 'Milam' }, { fips: 333, name: 'Mills' }, { fips: 335, name: 'Mitchell' }, { fips: 337, name: 'Montague' }, { fips: 339, name: 'Montgomery' }, { fips: 341, name: 'Moore' }, { fips: 343, name: 'Morris' }, { fips: 345, name: 'Motley' }, { fips: 347, name: 'Nacogdoches' }, { fips: 349, name: 'Navarro' }, { fips: 351, name: 'Newton' }, { fips: 353, name: 'Nolan' }, { fips: 355, name: 'Nueces' }, { fips: 357, name: 'Ochiltree' }, { fips: 359, name: 'Oldham' }, { fips: 361, name: 'Orange' }, { fips: 363, name: 'Palo Pinto' }, { fips: 365, name: 'Panola' }, { fips: 367, name: 'Parker' }, { fips: 369, name: 'Parmer' }, { fips: 371, name: 'Pecos' }, { fips: 373, name: 'Polk' }, { fips: 375, name: 'Potter' }, { fips: 377, name: 'Presidio' }, { fips: 379, name: 'Rains' }, { fips: 381, name: 'Randall' }, { fips: 383, name: 'Reagan' }, { fips: 385, name: 'Real' }, { fips: 387, name: 'Red River' }, { fips: 389, name: 'Reeves' }, { fips: 391, name: 'Refugio' }, { fips: 393, name: 'Roberts' }, { fips: 395, name: 'Robertson' }, { fips: 397, name: 'Rockwall' }, { fips: 399, name: 'Runnels' }, { fips: 401, name: 'Rusk' }, { fips: 403, name: 'Sabine' }, { fips: 405, name: 'San Augustine' }, { fips: 407, name: 'San Jacinto' }, { fips: 409, name: 'San Patricio' }, { fips: 411, name: 'San Saba' }, { fips: 413, name: 'Schleicher' }, { fips: 415, name: 'Scurry' }, { fips: 417, name: 'Shackelford' }, { fips: 419, name: 'Shelby' }, { fips: 421, name: 'Sherman' }, { fips: 423, name: 'Smith' }, { fips: 425, name: 'Somervell' }, { fips: 427, name: 'Starr' }, { fips: 429, name: 'Stephens' }, { fips: 431, name: 'Sterling' }, { fips: 433, name: 'Stonewall' }, { fips: 435, name: 'Sutton' }, { fips: 437, name: 'Swisher' }, { fips: 439, name: 'Tarrant' }, { fips: 441, name: 'Taylor' }, { fips: 443, name: 'Terrell' }, { fips: 445, name: 'Terry' }, { fips: 447, name: 'Throckmorton' }, { fips: 449, name: 'Titus' }, { fips: 451, name: 'Tom Green' }, { fips: 453, name: 'Travis' }, { fips: 455, name: 'Trinity' }, { fips: 457, name: 'Tyler' }, { fips: 459, name: 'Upshur' }, { fips: 461, name: 'Upton' }, { fips: 463, name: 'Uvalde' }, { fips: 465, name: 'Val Verde' }, { fips: 467, name: 'Van Zandt' }, { fips: 469, name: 'Victoria' }, { fips: 471, name: 'Walker' }, { fips: 473, name: 'Waller' }, { fips: 475, name: 'Ward' }, { fips: 477, name: 'Washington' }, { fips: 479, name: 'Webb' }, { fips: 481, name: 'Wharton' }, { fips: 483, name: 'Wheeler' }, { fips: 485, name: 'Wichita' }, { fips: 487, name: 'Wilbarger' }, { fips: 489, name: 'Willacy' }, { fips: 491, name: 'Williamson' }, { fips: 493, name: 'Wilson' }, { fips: 495, name: 'Winkler' }, { fips: 497, name: 'Wise' }, { fips: 499, name: 'Wood' }, { fips: 501, name: 'Yoakum' }, { fips: 503, name: 'Young' }, { fips: 505, name: 'Zapata' }, { fips: 507, name: 'Zavala' }],
        'Utah': [{ fips: 1, name: 'Beaver' }, { fips: 3, name: 'Box Elder' }, { fips: 5, name: 'Cache' }, { fips: 7, name: 'Carbon' }, { fips: 9, name: 'Daggett' }, { fips: 11, name: 'Davis' }, { fips: 13, name: 'Duchesne' }, { fips: 15, name: 'Emery' }, { fips: 17, name: 'Garfield' }, { fips: 19, name: 'Grand' }, { fips: 21, name: 'Iron' }, { fips: 23, name: 'Juab' }, { fips: 25, name: 'Kane' }, { fips: 27, name: 'Millard' }, { fips: 29, name: 'Morgan' }, { fips: 31, name: 'Piute' }, { fips: 33, name: 'Rich' }, { fips: 35, name: 'Salt Lake' }, { fips: 37, name: 'San Juan' }, { fips: 39, name: 'Sanpete' }, { fips: 41, name: 'Sevier' }, { fips: 43, name: 'Summit' }, { fips: 45, name: 'Tooele' }, { fips: 47, name: 'Uintah' }, { fips: 49, name: 'Utah' }, { fips: 51, name: 'Wasatch' }, { fips: 53, name: 'Washington' }, { fips: 55, name: 'Wayne' }, { fips: 57, name: 'Weber' }],
        'Vermont': [{ fips: 1, name: 'Addison' }, { fips: 3, name: 'Bennington' }, { fips: 5, name: 'Caledonia' }, { fips: 7, name: 'Chittenden' }, { fips: 9, name: 'Essex' }, { fips: 11, name: 'Franklin' }, { fips: 13, name: 'Grand Isle' }, { fips: 15, name: 'Lamoille' }, { fips: 17, name: 'Orange' }, { fips: 19, name: 'Orleans' }, { fips: 21, name: 'Rutland' }, { fips: 23, name: 'Washington' }, { fips: 25, name: 'Windham' }, { fips: 27, name: 'Windsor' }],
        'Virginia': [{ fips: 1, name: 'Accomack' }, { fips: 3, name: 'Albemarle' }, { fips: 5, name: 'Alleghany' }, { fips: 7, name: 'Amelia' }, { fips: 9, name: 'Amherst' }, { fips: 11, name: 'Appomattox' }, { fips: 13, name: 'Arlington' }, { fips: 15, name: 'Augusta' }, { fips: 17, name: 'Bath' }, { fips: 19, name: 'Bedford' }, { fips: 21, name: 'Bland' }, { fips: 23, name: 'Botetourt' }, { fips: 25, name: 'Brunswick' }, { fips: 27, name: 'Buchanan' }, { fips: 29, name: 'Buckingham' }, { fips: 31, name: 'Campbell' }, { fips: 33, name: 'Caroline' }, { fips: 35, name: 'Carroll' }, { fips: 36, name: 'Charles City' }, { fips: 37, name: 'Charlotte' }, { fips: 41, name: 'Chesterfield' }, { fips: 43, name: 'Clarke' }, { fips: 45, name: 'Craig' }, { fips: 47, name: 'Culpeper' }, { fips: 49, name: 'Cumberland' }, { fips: 51, name: 'Dickenson' }, { fips: 53, name: 'Dinwiddie' }, { fips: 57, name: 'Essex' }, { fips: 59, name: 'Fairfax' }, { fips: 61, name: 'Fauquier' }, { fips: 63, name: 'Floyd' }, { fips: 65, name: 'Fluvanna' }, { fips: 67, name: 'Franklin' }, { fips: 69, name: 'Frederick' }, { fips: 71, name: 'Giles' }, { fips: 73, name: 'Gloucester' }, { fips: 75, name: 'Goochland' }, { fips: 77, name: 'Grayson' }, { fips: 79, name: 'Greene' }, { fips: 81, name: 'Greensville' }, { fips: 83, name: 'Halifax' }, { fips: 85, name: 'Hanover' }, { fips: 87, name: 'Henrico' }, { fips: 89, name: 'Henry' }, { fips: 91, name: 'Highland' }, { fips: 93, name: 'Isle of Wight' }, { fips: 95, name: 'James City' }, { fips: 97, name: 'King and Queen' }, { fips: 99, name: 'King George' }, { fips: 101, name: 'King William' }, { fips: 103, name: 'Lancaster' }, { fips: 105, name: 'Lee' }, { fips: 107, name: 'Loudoun' }, { fips: 109, name: 'Louisa' }, { fips: 111, name: 'Lunenburg' }, { fips: 113, name: 'Madison' }, { fips: 115, name: 'Mathews' }, { fips: 117, name: 'Mecklenburg' }, { fips: 119, name: 'Middlesex' }, { fips: 121, name: 'Montgomery' }, { fips: 125, name: 'Nelson' }, { fips: 127, name: 'New Kent' }, { fips: 131, name: 'Northampton' }, { fips: 133, name: 'Northumberland' }, { fips: 135, name: 'Nottoway' }, { fips: 137, name: 'Orange' }, { fips: 139, name: 'Page' }, { fips: 141, name: 'Patrick' }, { fips: 143, name: 'Pittsylvania' }, { fips: 145, name: 'Powhatan' }, { fips: 147, name: 'Prince Edward' }, { fips: 149, name: 'Prince George' }, { fips: 153, name: 'Prince William' }, { fips: 155, name: 'Pulaski' }, { fips: 157, name: 'Rappahannock' }, { fips: 159, name: 'Richmond' }, { fips: 161, name: 'Roanoke' }, { fips: 163, name: 'Rockbridge' }, { fips: 165, name: 'Rockingham' }, { fips: 167, name: 'Russell' }, { fips: 169, name: 'Scott' }, { fips: 171, name: 'Shenandoah' }, { fips: 173, name: 'Smyth' }, { fips: 175, name: 'Southampton' }, { fips: 177, name: 'Spotsylvania' }, { fips: 179, name: 'Stafford' }, { fips: 181, name: 'Surry' }, { fips: 183, name: 'Sussex' }, { fips: 185, name: 'Tazewell' }, { fips: 187, name: 'Warren' }, { fips: 191, name: 'Washington' }, { fips: 193, name: 'Westmoreland' }, { fips: 195, name: 'Wise' }, { fips: 197, name: 'Wythe' }, { fips: 199, name: 'York' }, { fips: 510, name: 'Alexandria City' }, { fips: 515, name: 'Bedford City' }, { fips: 520, name: 'Bristol City' }, { fips: 530, name: 'Buena Vista City' }, { fips: 540, name: 'Charlottesville City' }, { fips: 550, name: 'Chesapeake City' }, { fips: 560, name: 'Clifton Forge City' }, { fips: 570, name: 'Colonial Heights City' }, { fips: 580, name: 'Covington City' }, { fips: 590, name: 'Danville City' }, { fips: 595, name: 'Emporia City' }, { fips: 600, name: 'Fairfax City' }, { fips: 610, name: 'Falls Church City' }, { fips: 620, name: 'Franklin City' }, { fips: 630, name: 'Fredericksburg City' }, { fips: 640, name: 'Galax City' }, { fips: 650, name: 'Hampton City' }, { fips: 660, name: 'Harrisonburg City' }, { fips: 670, name: 'Hopewell City' }, { fips: 678, name: 'Lexington City' }, { fips: 680, name: 'Lynchburg City' }, { fips: 683, name: 'Manassas City' }, { fips: 685, name: 'Manassas Park City' }, { fips: 690, name: 'Martinsville City' }, { fips: 700, name: 'Newport News City' }, { fips: 710, name: 'Norfolk City' }, { fips: 720, name: 'Norton City' }, { fips: 730, name: 'Petersburg City' }, { fips: 735, name: 'Poquoson City' }, { fips: 740, name: 'Portsmouth City' }, { fips: 750, name: 'Radford' }, { fips: 760, name: 'Richmond City' }, { fips: 770, name: 'Roanoke City' }, { fips: 775, name: 'Salem City' }, { fips: 780, name: 'South Boston City' }, { fips: 790, name: 'Staunton City' }, { fips: 800, name: 'Suffolk City' }, { fips: 810, name: 'Virginia Beach City' }, { fips: 820, name: 'Waynesboro City' }, { fips: 830, name: 'Williamsburg City' }, { fips: 840, name: 'Winchester City' }],
        'Washington': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Asotin' }, { fips: 5, name: 'Benton' }, { fips: 7, name: 'Chelan' }, { fips: 9, name: 'Clallam' }, { fips: 11, name: 'Clark' }, { fips: 13, name: 'Columbia' }, { fips: 15, name: 'Cowlitz' }, { fips: 17, name: 'Douglas' }, { fips: 19, name: 'Ferry' }, { fips: 21, name: 'Franklin' }, { fips: 23, name: 'Garfield' }, { fips: 25, name: 'Grant' }, { fips: 27, name: 'Grays Harbor' }, { fips: 29, name: 'Island' }, { fips: 31, name: 'Jefferson' }, { fips: 33, name: 'King' }, { fips: 35, name: 'Kitsap' }, { fips: 37, name: 'Kittitas' }, { fips: 39, name: 'Klickitat' }, { fips: 41, name: 'Lewis' }, { fips: 43, name: 'Lincoln' }, { fips: 45, name: 'Mason' }, { fips: 47, name: 'Okanogan' }, { fips: 49, name: 'Pacific' }, { fips: 51, name: 'Pend Oreille' }, { fips: 53, name: 'Pierce' }, { fips: 55, name: 'San Juan' }, { fips: 57, name: 'Skagit' }, { fips: 59, name: 'Skamania' }, { fips: 61, name: 'Snohomish' }, { fips: 63, name: 'Spokane' }, { fips: 65, name: 'Stevens' }, { fips: 67, name: 'Thurston' }, { fips: 69, name: 'Wahkiakum' }, { fips: 71, name: 'Walla Walla' }, { fips: 73, name: 'Whatcom' }, { fips: 75, name: 'Whitman' }, { fips: 77, name: 'Yakima' }],
        'West Virginia': [{ fips: 1, name: 'Barbour' }, { fips: 3, name: 'Berkeley' }, { fips: 5, name: 'Boone' }, { fips: 7, name: 'Braxton' }, { fips: 9, name: 'Brooke' }, { fips: 11, name: 'Cabell' }, { fips: 13, name: 'Calhoun' }, { fips: 15, name: 'Clay' }, { fips: 17, name: 'Doddridge' }, { fips: 19, name: 'Fayette' }, { fips: 21, name: 'Gilmer' }, { fips: 23, name: 'Grant' }, { fips: 25, name: 'Greenbrier' }, { fips: 27, name: 'Hampshire' }, { fips: 29, name: 'Hancock' }, { fips: 31, name: 'Hardy' }, { fips: 33, name: 'Harrison' }, { fips: 35, name: 'Jackson' }, { fips: 37, name: 'Jefferson' }, { fips: 39, name: 'Kanawha' }, { fips: 41, name: 'Lewis' }, { fips: 43, name: 'Lincoln' }, { fips: 45, name: 'Logan' }, { fips: 47, name: 'McDowell' }, { fips: 49, name: 'Marion' }, { fips: 51, name: 'Marshall' }, { fips: 53, name: 'Mason' }, { fips: 55, name: 'Mercer' }, { fips: 57, name: 'Mineral' }, { fips: 59, name: 'Mingo' }, { fips: 61, name: 'Monongalia' }, { fips: 63, name: 'Monroe' }, { fips: 65, name: 'Morgan' }, { fips: 67, name: 'Nicholas' }, { fips: 69, name: 'Ohio' }, { fips: 71, name: 'Pendleton' }, { fips: 73, name: 'Pleasants' }, { fips: 75, name: 'Pocahontas' }, { fips: 77, name: 'Preston' }, { fips: 79, name: 'Putnam' }, { fips: 81, name: 'Raleigh' }, { fips: 83, name: 'Randolph' }, { fips: 85, name: 'Ritchie' }, { fips: 87, name: 'Roane' }, { fips: 89, name: 'Summers' }, { fips: 91, name: 'Taylor' }, { fips: 93, name: 'Tucker' }, { fips: 95, name: 'Tyler' }, { fips: 97, name: 'Upshur' }, { fips: 99, name: 'Wayne' }, { fips: 101, name: 'Webster' }, { fips: 103, name: 'Wetzel' }, { fips: 105, name: 'Wirt' }, { fips: 107, name: 'Wood' }, { fips: 109, name: 'Wyoming' }],
        'Wisconsin': [{ fips: 1, name: 'Adams' }, { fips: 3, name: 'Ashland' }, { fips: 5, name: 'Barron' }, { fips: 7, name: 'Bayfield' }, { fips: 9, name: 'Brown' }, { fips: 11, name: 'Buffalo' }, { fips: 13, name: 'Burnett' }, { fips: 15, name: 'Calumet' }, { fips: 17, name: 'Chippewa' }, { fips: 19, name: 'Clark' }, { fips: 21, name: 'Columbia' }, { fips: 23, name: 'Crawford' }, { fips: 25, name: 'Dane' }, { fips: 27, name: 'Dodge' }, { fips: 29, name: 'Door' }, { fips: 31, name: 'Douglas' }, { fips: 33, name: 'Dunn' }, { fips: 35, name: 'Eau Claire' }, { fips: 37, name: 'Florence' }, { fips: 39, name: 'Fond Du Lac' }, { fips: 41, name: 'Forest' }, { fips: 43, name: 'Grant' }, { fips: 45, name: 'Green' }, { fips: 47, name: 'Green Lake' }, { fips: 49, name: 'Iowa' }, { fips: 51, name: 'Iron' }, { fips: 53, name: 'Jackson' }, { fips: 55, name: 'Jefferson' }, { fips: 57, name: 'Juneau' }, { fips: 59, name: 'Kenosha' }, { fips: 61, name: 'Kewaunee' }, { fips: 63, name: 'La Crosse' }, { fips: 65, name: 'Lafayette' }, { fips: 67, name: 'Langlade' }, { fips: 69, name: 'Lincoln' }, { fips: 71, name: 'Manitowoc' }, { fips: 73, name: 'Marathon' }, { fips: 75, name: 'Marinette' }, { fips: 77, name: 'Marquette' }, { fips: 78, name: 'Menominee' }, { fips: 79, name: 'Milwaukee' }, { fips: 81, name: 'Monroe' }, { fips: 83, name: 'Oconto' }, { fips: 85, name: 'Oneida' }, { fips: 87, name: 'Outagamie' }, { fips: 89, name: 'Ozaukee' }, { fips: 91, name: 'Pepin' }, { fips: 93, name: 'Pierce' }, { fips: 95, name: 'Polk' }, { fips: 97, name: 'Portage' }, { fips: 99, name: 'Price' }, { fips: 101, name: 'Racine' }, { fips: 103, name: 'Richland' }, { fips: 105, name: 'Rock' }, { fips: 107, name: 'Rusk' }, { fips: 109, name: 'St Croix' }, { fips: 111, name: 'Sauk' }, { fips: 113, name: 'Sawyer' }, { fips: 115, name: 'Shawano' }, { fips: 117, name: 'Sheboygan' }, { fips: 119, name: 'Taylor' }, { fips: 121, name: 'Trempealeau' }, { fips: 123, name: 'Vernon' }, { fips: 125, name: 'Vilas' }, { fips: 127, name: 'Walworth' }, { fips: 129, name: 'Washburn' }, { fips: 131, name: 'Washington' }, { fips: 133, name: 'Waukesha' }, { fips: 135, name: 'Waupaca' }, { fips: 137, name: 'Waushara' }, { fips: 139, name: 'Winnebago' }, { fips: 141, name: 'Wood' }],
        'Wyoming': [{ fips: 1, name: 'Albany' }, { fips: 3, name: 'Big Horn' }, { fips: 5, name: 'Campbell' }, { fips: 7, name: 'Carbon' }, { fips: 9, name: 'Converse' }, { fips: 11, name: 'Crook' }, { fips: 13, name: 'Fremont' }, { fips: 15, name: 'Goshen' }, { fips: 17, name: 'Hot Springs' }, { fips: 19, name: 'Johnson' }, { fips: 21, name: 'Laramie' }, { fips: 23, name: 'Lincoln' }, { fips: 25, name: 'Natrona' }, { fips: 27, name: 'Niobrara' }, { fips: 29, name: 'Park' }, { fips: 31, name: 'Platte' }, { fips: 33, name: 'Sheridan' }, { fips: 35, name: 'Sublette' }, { fips: 37, name: 'Sweetwater' }, { fips: 39, name: 'Teton' }, { fips: 41, name: 'Uinta' }, { fips: 43, name: 'Washakie' }, { fips: 45, name: 'Weston' }]

    }


    //selected = "selected"



    var $Counties1 = $('#CountyFips');

    //var myCounty = $("#County :selected").text();
    var myCounty = $("#CountyFips").val();

    //var mystate1 = $("#UsState :selected").text();
    var stateVal = $('#UsState').val();

    if (stateVal > 0) {
        var state1 = $("#UsState :selected").text(), lcns1 = locations[state1] || [];

        var html1 = $.map(lcns1, function (lcn1) {
            if (lcn1.fips == myCounty) {
                return '<option selected = "selected" value="' + lcn1.fips + '">' + lcn1.name + '</option>';
            }
            else {
                return '<option value="' + lcn1.fips + '">' + lcn1.name + '</option>';
            }
        }).join('');
        $Counties1.html(html1)
    }
};

