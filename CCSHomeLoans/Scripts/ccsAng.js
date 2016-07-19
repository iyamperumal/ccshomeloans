'use strict';
ccsApp.controller('optionsController',
   function optionsController($scope, $http, $window) {
       //alert("in the controler");
       $scope.data = [];
       var appOption;
       $http.get("/Mortgages1/GetOptions?includeReplies=true")
              .then(function (result) {
                  //Successful
                  angular.copy(result.data, $scope.data);
                  //alert("got Data");
              },
              function () {
                  //error
                  alert("could not get data");
              });



       $scope.optionSelected = function(option) {

           option.OptionName += option.VarNum;

           $scope.SaveSelectedOption(option, $window);

       };

       $scope.SaveSelectedOption = function (selectedOption, $window) {
           //appOption = selectedOption;

           $http.post("/Mortgages1/SaveSelectedOption", selectedOption)
               .then(function (result) {

                   var selectedOption = result.data;

                   alert("option saves");

               },

               function () {

                   //error
                   //alert("could not Save");

               });

           $window.location = "/Mortgages1/selectedOption";
           //$location.path('/RefiOptions/selectedOption');

       };
            
   });

