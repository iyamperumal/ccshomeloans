//'use strict';

////ccsApp.controller('AppController',
////    function AppController($scope) {

////        //$scope.loantype1 = "Refi";




////        function LoanTypeChanged($scope) {

////            var selection = $('#LoanTypeRequested').val(e);
////            selectiong = e.val;

////            $scope.loantype1 = "hello2";


////            $scope.myvar = "in the event"

////            $scope.loantype1 = selection;

////        };

////    }

////);


//Debt Consolidation: Pay Off Creditors  LoanTypeRequested

function LoanTypeChanged2() {

    var selecteditem = $("#LoanTypeRequested option:selected").text();


    //Realtor Referred Purchase Loan

    if (selecteditem === "Realtor Referred Purchase Loan") {
        $('#realtorId').show(600);

    }
    else {

        $('#realtorId').hide(600);

    }


    if (selecteditem === "Purchase Loan" | selecteditem === "Realtor Referred Purchase Loan") {
        $('#purshasePriceId').show(600);
        $('#estimatedHoaId').show(600);
        $('#downPaymentId').show(600);

    }
    else {

        $('#purshasePriceId').hide(600);
        $('#estimatedHoaId').hide(600);
        $('#downPaymentId').hide(600);
    }



    if (selecteditem === "Debt Consolidation: Pay Off Creditors") {
        $('#estimatedDebtToPayId').show(600);
        $('#totalPmttopayOffId').show(600);
        $('#additionalCashOutId').show(600);


    }
    else {

        $('#estimatedDebtToPayId').hide(600);
        $('#totalPmttopayOffId').hide(600);
        $('#additionalCashOutId').hide(600);

    }



    if (selecteditem === "Cash Out Mortgage") {
        $('#CashoutId').show(600);
    }

    else {
        $('#CashoutId').hide(600);
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
    }

    else {
        $('#dateOfbankruptcyId').hide(600);
    }



    if (selecteditem2 === "Chapter 13 Repayment Still Open") {
        $('#Chapter13Id').show(600);
    }

    else {
        $('#Chapter13Id').hide(600);
    }



};

//Anyforeclosures

function AnyForeclosures() {

    var selecteditem3 = $("#ForeclosuresShortSaleDeedinLieu option:selected").text();


    if (selecteditem3 === "Yes Forclosure" | selecteditem3 === "Yes Short Sale" | selecteditem3 === "Yes Deed and Lieu") {
        $('#dateofForeclosureId').show(600);
    }

    else {
        $('#dateofForeclosureId').hide(600);
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



    //$('#app1').hide(600);
    //$('#app2').show(600);

    $('#app3').slideToggle('slow');
    $('#app4').slideToggle('slow');


    // $('#app1').hide('slide',{direction:'left'},'1000');
    // $('#app2').show('slide', { direction: 'left' });

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

    var secondmgitem = $("#Have2ndMortgage option:selected").text();

    //secondMgPymtId
    //secondMgTermId
    //secondmgTypeId
    //secondMgInterestId
    //secondMgBalId
    //payOff2ndId

    if (secondmgitem === "Yes") {
        $('#secondMgPymtId').show(600);
        $('#secondMgTermId').show(600);
        $('#secondmgTypeId').show(600);
        $('#secondMgInterestId').show(600);
        $('#secondMgBalId').show(600);
        $('#payOff2ndId').show(600);
    }
    else {
        $('#secondMgPymtId').hide(600);
        $('#secondMgTermId').hide(600);
        $('#secondmgTypeId').hide(600);
        $('#secondMgInterestId').hide(600);
        $('#secondMgBalId').hide(600);
        $('#payOff2ndId').hide(600);
    }

};

//homeownersAssocId
function MortgageInsChk() {

    //var mgInsChk = $('#mortgageInsChkId')

    if ($('#mortgageInsChkId').is(':checked')) {
        $('#monthlyMgInsId').show(600);
    }
    else {
        $('#monthlyMgInsId').hide(600);
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