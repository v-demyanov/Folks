const usernameField = document.getElementById('Input_UserName');
const passwordField = document.getElementById('Input_Password');
const usernameInvalidTooltip = document.getElementById('username-invalid-tooltip');
const passwordInvalidTooltip = document.getElementById('password-invalid-tooltip');
const form = document.getElementById('login-form');
const submitBtn = document.getElementById('submit-btn');

usernameField.addEventListener('input', () => {
    const isValid = usernameField.checkValidity();

    if (isValid) {
        usernameField.classList.add('is-valid');
        usernameField.classList.remove('is-invalid');
    } else {
        usernameField.classList.add('is-invalid');
        usernameField.classList.remove('is-valid');
    }
});

passwordField.addEventListener('input', () => {
    const isValid = passwordField.checkValidity();

    if (isValid) {
        passwordField.classList.add('is-valid');
        passwordField.classList.remove('is-invalid');
    } else {
        passwordField.classList.add('is-invalid');
        passwordField.classList.remove('is-valid');
    }
});

usernameField.addEventListener('invalid', (event) => {
    usernameInvalidTooltip.innerHTML = event.target.validationMessage;
});

passwordField.addEventListener('invalid', (event) => {
    passwordInvalidTooltip.innerHTML = event.target.validationMessage;
});

form.addEventListener('input', () => {
    const isValid = form.checkValidity();

    if (isValid) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});