var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
        $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.Employe = {};

            if ($scope.EmpName == '' || $scope.EmpName == undefined) {
                alert("Please enter name ");
                return false;
            }
            else {
                $scope.Employe.Emp_Name = $scope.EmpName;
            }
            if ($scope.EmpCity == '' || $scope.EmpCity == undefined) {
                alert("Please enter city ");
                return false;
            }
            else {
                $scope.Employe.Emp_City = $scope.EmpCity;
            }
            if ($scope.EmpAge == '' || $scope.EmpAge == undefined) {
                alert("Please enter age ");
                return false;
            }
            else {
                $scope.Employe.Emp_Age = $scope.EmpAge;
            }             
               $http({
                method: "post",
                url: "/Home/Insert_data",
                datatype: "json",
                data: JSON.stringify($scope.Employe)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.EmpName = "";
                $scope.EmpCity = "";
                $scope.EmpAge = "";
            })
        } else {
            $scope.Employe = {};
            $scope.Employe.Emp_Name = $scope.EmpName;
            $scope.Employe.Emp_City = $scope.EmpCity;
            $scope.Employe.Emp_Age = $scope.EmpAge;
            $scope.Employe.Emp_Id = document.getElementById("EmpID_").value;
            $http({
                method: "post",
                url: "/Home/update",
                datatype: "json",
                data: JSON.stringify($scope.Employe)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.EmpName = "";
                $scope.EmpCity = "";
                $scope.EmpAge = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Employee";
            })
        }
    }
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "/Home/GetAll"
        }).then(function (response) {
            $scope.employees = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.DeleteEmp = function (Emp) {
        debugger;
        $http({
            method: "post",
            url: "/Home/Delete_emp",
            datatype: "json",
            data: JSON.stringify(Emp)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.UpdateEmp = function (Emp) {
        document.getElementById("EmpID_").value = Emp.Emp_Id;
        $scope.EmpName = Emp.Emp_Name;
        $scope.EmpCity = Emp.Emp_City;
        $scope.EmpAge = Emp.Emp_Age;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Green";
        document.getElementById("spn").innerHTML = "Update Employee Information";
    }
})