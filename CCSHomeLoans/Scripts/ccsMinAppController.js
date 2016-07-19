'use strict';
ccsApp.controller('ccsMinApp',
    function ccsMinApp($scope, $http) {
        //alert("in the controler");

        $scope.option = [];

        //$scope.option = [
        //     {
        //         OptionSelected_Id: 7,
        //         PreparedFor: "MATSUE MAERZ",
        //         InterestRate: 3.875,
        //         OptionName: "Fannie HARP 30YR1",
        //         TermInYears: 30,
        //         NewMonthlyPaymentPrincipalInterest: 1172.51,
        //         LoanAmount: 249346.35,
        //         MonthlySavings: 739.48,
        //         TotalSavingsFromOldMortgageToNewMortgage: 235004,
        //         CostSavingsBreakEvenAnalysis: 10.46,
        //         VarNum: 1
        //     }
        //];



        $http.get("/Mortgages1/SelectedOptions?includeReplies=true")

            .then(function (result) {
                //Successful
                angular.copy(result.data, $scope.option)
            },

                function () {

                    //error
                    //alert("could not get data")

                });

    });