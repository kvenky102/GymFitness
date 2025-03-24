<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

        <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                
                /*background-color: #957cd8;
                background-color: #2596be;*/
                background-color: #28a745;
                color: #333;
                font-weight: bold;
                /*BackColor="#F7F7F7"*/
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }

        .Pager span {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 20px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }

        .Pager a {
            text-align: center;
            display: inline-block;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }

              .Pager1 span {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 20px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }

        .Pager1 a {
            text-align: center;
            display: inline-block;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }

        .highlight {
            background-color: #FFFFAF;
        }
    </style>

      
    

    <style type="text/css">
        .scrolling {
            position: absolute;
        }

        .gvWidthHight {
            overflow: scroll;
            height: 800px;
            width: 800px;
        }
        
        .display-hidden {
         display: none !important; 

        }

        table, td, th {  
  border: 1px solid #ddd;
  text-align: left;
}

table {
  border-collapse: collapse;
  width: 55%;
}

th, td {
  padding: 15px;
}
    </style>

        
    <!-- Bootstrap core CSS -->


    <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }


      /* Add these styles to your CSS file or in a <style> block */
/* Fix Bootstrap 5 conflicting styles */
.dataTables_wrapper {
    width: 100%;
}
/*
.dataTables_length,
.dataTables_filter {
    float: left !important;
    margin-bottom: 10px;
}*/

.dataTables_paginate {
    float: right !important;
}

.table-responsive {
    overflow-x: auto;
}

 

    </style>
<!-- jQuery (Required for DataTables) -->


<!-- Bootstrap 5 -->



<!-- DataTables for Bootstrap 5 -->

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdn.datatables.net/1.13.4/css/dataTables.bootstrap5.min.css" rel="stylesheet">

    
      <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js"></script>
    

          
 <div class="container mt-4">

        <h2 class="text-center">Membership Data</h2>
        <table id="membershipTable" class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Phone Number</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Remaining Days</th>
                    <th>Status</th>
                    <th>Subscription Type</th>
                    <th>ID</th>
                </tr>
                <tr>
                    <th><input type="text" class="form-control" placeholder="Search Name"></th>
                    <th><input type="text" class="form-control" placeholder="Search Phone"></th>
                    <th><input type="text" class="form-control" placeholder="Search Start Date"></th>
                    <th><input type="text" class="form-control" placeholder="Search End Date"></th>
                    <th><input type="text" class="form-control" placeholder="Search Days"></th>
                    <th><input type="text" class="form-control" placeholder="Search Status"></th>
                    <th><input type="text" class="form-control" placeholder="Search Subscription"></th>
                    <th><input type="text" class="form-control" placeholder="Search ID"></th>
                </tr>
            </thead>
            <tbody>
                <!-- Data will be populated dynamically -->
            </tbody>
        </table>

    <script>
        $(document).ready(function () {
          let table = $('#membershipTable').DataTable({
    "ajax": {
        "url": "WebForm2.aspx/Membership_Data_Fn", // Replace 'YourPage.aspx' with actual ASPX page
        "type": "POST",
        "contentType": "application/json",
        "dataType": "json",
                   dataSrc: function (json) {
                let parsedData = json.d ? JSON.parse(json.d) : json;
                if (!parsedData || !parsedData.data) {
                    console.error("⚠️ Parsed data is incorrect:", parsedData);
                    return [];
                }
                return parsedData.data;
            },
        "error": function (xhr, status, error) {
            console.error("AJAX Error:", status, error, xhr.responseText);
        }
    },
    "columns": [
        { "data": "Name" },
        { "data": "Phone Number" },
        { "data": "Start Date" },
        { "data": "End Date" },
        { "data": "Remaining Days" },
        { "data": "Status" },
        { "data": "Subscription Type" },
        { "data": "Id" }
    ]
});



            $('#membershipTable thead input').on('keyup change', function () {
                let index = $(this).parent().index();
                table.column(index).search(this.value).draw();
            });
        });
    </script>

                </div>


      


</asp:Content>
