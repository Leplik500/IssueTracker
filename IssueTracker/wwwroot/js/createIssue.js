// jQuery('#registerForm').validate({
//         rules: {
//             password: {
//                 required: true,
//                 rangelength: [8, 32],
//                 pattern: /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[@$!%*?&]).{8,32})\S$/,
//             },
//             email: {
//                 required: true,
//                 email: true,
//                 rangelength: [3, 32],
//             },
//             confirmPassword: {
//                 equalTo: "#password",
//                 required: true,
//                 rangelength: [8, 32],
//             },
//             age: {
//                 min: 18,
//                 max: 10000,
//                 number: true
//             },
//             firstName: {
//                 required: true,
//                 pattern: /^[A-Z][a-z]*$/,
//                 rangelength: [3, 16],
//             },
//             lastName: {
//                 required: false,
//                 pattern: /^[A-Z][a-z]*$/,
//                 rangelength: [3, 16],
//             }
//         },
//         messages: {
//             password: {
//                 required: "Please enter a password",
//                 rangelength: "Your password must be between 8 and 32 characters long",
//                 pattern: "Your password must contain at least one uppercase, one lowercase, one number and one special character"
//             },
//             email: {
//                 required: "Please enter an email",
//                 email: "Please enter a valid email",
//                 rangelength: "Your email must be between 3 and 32 characters long"
//             },
//             confirmPassword: {
//                 equalTo: "Passwords are not equal",
//                 required: "Please enter a password",
//                 rangelength: "Your password must be between 8 and 32 characters long"
//             },
//             age: {
//                 min: "You must be at least 18 years old",
//                 max: "You must be less than 10000 years old"
//             },
//             firstName: {
//                 required: "Please enter a first name",
//                 pattern: "Your first name must contain only letters and first letter must be capitalized",
//                 rangelength: "Your first name must be between 3 and 16 characters long",
//             },
//             lastName: {
//                 // required: "Please enter a last name",
//                 pattern: "Your last name must contain only letters and first letter must be capitalized",
//                 rangelength: "Your last name must be between 3 and 16 characters long",
//             }
//         },
//         onfocusout: function (element) {
//             this.element(element); // triggers validation
//         },
//         onkeyup: function (element) {
//             this.element(element); // triggers validation
//         }
//     }
// )

$('#createIssue').on('click', function (event) {
    event.preventDefault();
    debugger
    $.ajax({
        url: '/App/Create',
        method: 'POST',
        data: $('#createIssueForm').serialize(),
        success: function (response) {
            alert("Issue created successfully")
            console.log(response)
            window.location.href = '/App/Issues';
        },
        error: function (response) {
            alert("Issue not created: " + response.responseJSON.description)
            console.log(response)
        }
    })
});
