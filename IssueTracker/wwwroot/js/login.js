$(function () {
    $('#loginForm').validate({
            rules: {
                password: {
                    required: true,
                    rangelength: [8, 32],
                    pattern: /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[@$!%*?&]).{8,32})\S$/,
                    password: true
                },
                email: {
                    required: true,
                    rangelength: [8, 32],
                    pattern: /^[\w\-.]+@([\w-]+\.)+[\w-]{2,}$/,
                    email: true
                }
            },
            messages: {
                password: {
                    required: "Please enter a password",
                    pattern: "Your password must contain at least one uppercase letter, one lowercase letter, one number and one special character",
                    rangelength: "Your password must be between 8 and 32 characters long"
                }
            },
            onfocusout: function (element) {
                this.element(element); // triggers validation
            },
            onkeyup: function (element, event) {
                this.element(element); // triggers validation
            }
        }
    )
});