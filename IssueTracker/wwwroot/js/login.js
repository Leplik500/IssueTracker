jQuery('#loginForm').validate({
        rules: {
            password: {
                required: true,
                rangelength: [8, 32],
                pattern: /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[@$!%*?&]).{8,32})\S$/,
            },
            email: {
                required: true,
                email: true,
                rangelength: [3, 32],
            },
        },
        messages: {
            password: {
                required: "Please enter a password",
                rangelength: "Your password must be between 8 and 32 characters long",
                pattern: "Your password must contain at least one uppercase, one lowercase, one number and one special character"
            },
            email: {
                required: "Please enter an email",
                email: "Please enter a valid email",
                rangelength: "Your email must be between 3 and 32 characters long"
            },
        },
        onfocusout: function (element) {
            this.element(element); // triggers validation
        },
        onkeyup: function (element) {
            this.element(element); // triggers validation
        }
    }
)

// $('#login').on('click', function (event) {
//     event.preventDefault();
//     $.ajax({
//         url: '/Create',
//         method: 'POST',
//         data: $('#registerForm').serialize(),
//         success: function (response) {
//             alert("User created successfully")
//             console.log(response)
//         },
//         error: function (response) {
//             alert("User not created")
//             console.log(response)
//         }
//     })
// });
