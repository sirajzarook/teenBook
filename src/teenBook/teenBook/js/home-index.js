//home-index.js

var module = angular.module('homeIndex', ["ngRoute"]);
//app.controller('topicsController',topicsController); //register the control

module.config(function ($routeProvider, $locationProvider) {

    // added with locationProvider code
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });

    // these two lines was part of the fix it adding #%2f and ! to url, it fixes all navigation errors
    $locationProvider.html5Mode(false);
    $locationProvider.hashPrefix('');

    $routeProvider
    .when("/", {
        controller: "topicsController",
        templateUrl: "/templates/topicsView.html"
    });

    $routeProvider
    .when("/newmessage", {
        controller: "newTopicController",
        templateUrl: "/templates/newTopicView.html"
    });

    $routeProvider
    .when("/message/:id", {
        controller: "singleTopicController",
        templateUrl: "/templates/singleTopicView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});

module.factory("dataService", function ($http, $q) {
    // inject $q, $q is an object allow us to create a 'defer' object, then 'defer' object allows us to have a promise
    // whether some call is success or errored is handled in Angular JS is called a promise.
    // AngularJS is has something call difer which allows you to implement your own promise.
    // you inject the dataServices as parameter in other module
    // public instances like Topic is singleton, only one instance of it for the app 

    var _topics = []; //a private to this. a data collection i.e. topics
    var _isInit = false;
    var _isReady = false;

    var _isReady = function () {
        return _isInit;
    }

    var _getTopics = function () {

        var deferred = $q.defer();

        $http.get("api/v1/topics?includereplies=true")
            .then(function (result) {
                //when success

                //$scope.data = result.data; // would work in normal cases like not dealing with collection

                // angular way to deal with collection of data (helps repeat calls in UI pages
                angular.copy(result.data, _topics);
                _isInit = true;
                deferred.resolve();

            },
            function () {
                //when failed, from service you cann raise alerts, so return a promise to calling function
                //alert("could not load the topics");
                deffered.reject();

            });
        return deferred.promise;
    };

    //addTopic Service
    var _addTopic = function (newTopic) {
        var deferred = $q.defer();

        $http.post("/api/v1/topics", newTopic)
                      .then(function (result) {
                          //success
                          var newlyCreatedTopic = result.data;
                          _topics.splice(0, 0, newlyCreatedTopic);
                          deferred.resolve(newlyCreatedTopic);
                          //TODO merge with the existing list of topics
                      },
                      function () {
                          deferred.reject();
                          //alert("cannot save the new topic");
                      });

        return deferred.promise;

    };

    //findTopic willbe implmented as internal private function
    function _findTopic(id) {
        
        var found = null;
        $.each(_topics, function(i, item){
            if (item.id == id) {
                found = item;
                return false; // this exist the each loop
            }
                
        });
        return found;
    }

    var _getTopicById = function(id){
        var deferred = $q.defer();
        
        if (_isReady == true) {
            var topic = _findTopic(id);
            if (topic) { // if topic not null
                deferred.resolve(topic);
            } else {
                deferred.reject();
            }

        } else { //if topics has been not loaded
        _getTopics()
            .then(function () {
                //when success
                var topic = _findTopic(id);
                if (topic) { // if topic not null
                    deferred.resolve(topic);
                } else {
                    deferred.reject();
                }
            },
                function () {
                    //when failed
                    deferred.reject();
                })
        }

        return deferred.promise;

    }

    //addReply Service
    var _saveReply = function (topic, newReply) {
        var deferred = $q.defer();

        $http.post("/api/v1/topics/" + topic.id + "/replies", newReply)
                      .then(function (result) {
                          //success
                          if (topic.replies == null) topic.replies = []; // in case thing being first reply
                          topic.replies.push(result.data); // add new reply to the topic
                          deferred.resolve(result.data); // at this returning the replies not requred
                          //TODO merge with the existing list of topics
                      },
                      function () {
                          deferred.reject();
                          //alert("cannot save the new topic");
                      });

        return deferred.promise;

    };

    return {
        //expose the data as public interface of this data service to other modules
        topics: _topics,
        getTopics: _getTopics,
        addTopic: _addTopic,
        isReady: _isReady,
        getTopicById: _getTopicById,
        saveReply: _saveReply

    };
});


module.controller('topicsController', function ($scope, $http, dataService) {
    //alert("Hello world angular");
    $scope.name = "Siraj Zarook";
    $scope.dataCount = 0;
    $scope.data = dataService; // binding the whole dataService insted of $dataServices.Topics to scope object will the UI updated/synced
    $scope.isBusy = false;

    if (dataService.isReady() == false) {
        $scope.isBusy = true;
        dataService.getTopics()
            .then(function () {
                 //when success
            },
            function () {
                //when failed
                 alert("could not load the topics");
            })
            .then(function () {

                 $scope.isBusy = false;

            });
        }
    //// for test use only
    // $scope.name = "Siraj Zarook";

    ////commenting hard coded values and will replaced by webAPI calls
    //$scope.data = [
    //{
    //    title: 'my title',
    //    body: 'Weekend in Madrid',
    //    created: '2017-02-22'       
    //},
    //{
    //    title: 'my title1',
    //    body: 'Weekend in Madrid1',
    //    created: '2017-02-23'       
    //}]

    //alert("Hello world angular1");
});

// window depedency injection help to return to home page
module.controller('newTopicController', function ($scope, $http, dataService, $window) {

    $scope.newTopic = {};

    $scope.save = function () {
        dataService.addTopic($scope.newTopic)
               .then(function () {
                   //success
                     //var newTopic = restult.data; // dataServices is doing this now
                   //TODO merge with the existing list of topics
                   $window.location = "#/";
               },
               function () {
                   alert("cannot save the new topic");
               });
    };
});

module.controller('singleTopicController', function ($scope, $http, dataService, $window, $routeParams) {

    $scope.topic = null;
    $scope.newReply = {};


    dataService.getTopicById($routeParams.id)
            .then(function (topic) {
                //success
                $scope.topic = topic;
            },
            function () {
                $window.location = "#/";

            });

    $scope.addReply = function () {

        dataService.saveReply($scope.topic, $scope.newReply)
                .then(function () {
                    //success
                    $scope.newReply.body = "";
                },
                function () {
                    alert("cannot save the new topic");
                });


    };

});