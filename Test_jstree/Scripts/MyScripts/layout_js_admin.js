$(document).ready(function () {
    console.log("Document Ready!"); // Debugging Check       
    alert("Document Ready!"); // Debugging Check

    // code kiểm tra tài khoản admin
    $('#mainForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission        
        $("#errorMsg").text("");
        $("#passwordInput").val("");
    });
    
    $("#verifyPasswordBtn").click(function () {
        
        var enteredPassword = $("#passwordInput").val();
        console.log("Entered Password: " + enteredPassword); // Debugging Check

        if (enteredPassword.length === 0) {
            $('#errorMsg').text('Password is required.');
            return;
        }        
        
        $.ajax({
            url: 'VerifyPassword',
            type: 'POST',
            data: { password: enteredPassword },
            success: function (response) {
                if (response.isValid) {
                    alert("Password verified successfully!");
                    
                    $("#passwordModal").modal('hide');
                    $('#mainForm').unbind('submit').submit(); // Submit the main form
                } else {
                    $("#errorMsg").text("Incorrect password!");
                }
            },
            error: function (xhr, status, error) {
                console.error("AJAX Error: ", error);
                $('#errorMsg').text('An error occurred while verifying the password.');
            }
        });
    });


    $(function () {
       $('#jstree').jstree({
           "plugins": ["checkbox"]  // enable checkbox plugin
       });

       // Optional: Handle checked nodes
       $('#jstree').on("changed.jstree", function (e, data) {
           var selectedIds = data.selected;
           console.log("Selected node IDs:", selectedIds);
       });
    });
})