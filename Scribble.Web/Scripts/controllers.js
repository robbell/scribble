(function () {
    'use strict';

    var scribbleControllers = angular.module('scribbleControllers', []);

    scribbleControllers.controller('CommentCtrl', [
        '$scope', '$http',
        function ($scope, $http) {
            $scope.comment = {};

            $scope.addComment = function (isValid) {
                if (isValid) {
                    $http.post('/comments/add', $scope.comment).success(function () {
                        alert("added!");
                    });
                }
            };
        }
    ]);
})();
