(function () {
    angular.module("humanGavelUser.app")
        .factory("casesService", casesService);

    function casesService($rootScope, $http) {


        this.getData = function (category, callback) {
            $http.get("api/content/cases/" + category)
                .then(function (res) {
                    callback(res);
                });
        };

        this.getSampleData = function () {
            return [{
                name: "Trump v. Clinton",
                voteData: [{ name: "Trump", value: 1212, participantId: 1 }, { name: "Clinton", value: 2323, participantId: 2 }],
                image: "Samples/One/ClintonTrump.jpg",
                caseEndDate: "8/20/2016",
                caseId: 0
            },
            {
                name: "Coke v. Pepsi",
                voteData: [{ name: "Coke", value: 1212, participantId: 3 }, { name: "Pepsi", value: 2323, participantId: 4 }],
                image: "Samples/Two/cokepepsi.jpg",
                caseEndDate: "1/1/2017",
                caseId: 1
            },
            {
                name: "Bills v. Dolphins",
                voteData: [{ name: "Bills", value: 1212, participantId: 5 }, { name: "Dolphins", value: 2323, participantId: 5 }],
                image: "Samples/Three/billsdolphins.jpg",
                caseEndDate: "1/1/2017",
                caseId: 2
            },
            {
                name: "Browns v. Bears",
                voteData: [{ name: "Browns", value: 7623, participantId: 7 }, { name: "Bears", value: 5411, participantId: 78 }],
                image: "Samples/Four/bears.jpeg",
                caseEndDate: "1/1/2017",
                caseId: 3
            }];
        };
            
        return this;
    }
})();