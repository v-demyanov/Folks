const usernameField = document.getElementById('Input_UserName');
const passwordField = document.getElementById('Input_Password');
const confirmPasswordField = document.getElementById('Input_ConfirmPassword');
const emailField = document.getElementById('Input_Email');

const usernameInvalidTooltip = document.getElementById('username-invalid-tooltip');
const passwordInvalidTooltip = document.getElementById('password-invalid-tooltip');
const confirmPasswordInvalidTooltip = document.getElementById('confirm-password-invalid-tooltip');
const emailInvalidTooltip = document.getElementById('email-invalid-tooltip');

const form = document.getElementById('register-form');
const submitBtn = document.getElementById('submit-btn');

usernameField.addEventListener('input', () => {
  const isValid = usernameField.checkValidity();

  if (isValid) {
    setFieldValidStyle(usernameField);
  } else {
    setFieldInvalidStyle(usernameField);
  }
});

passwordField.addEventListener('input', () => {
  const isValid = passwordField.checkValidity();
  const arePasswordsMatched = validatePasswordsMatching();

  if (arePasswordsMatched) {
    setFieldValidStyle(confirmPasswordField);
  } else {
    setFieldInvalidStyle(confirmPasswordField);
  }

  if (isValid) {
    setFieldValidStyle(passwordField);
  } else {
    setFieldInvalidStyle(passwordField);
  }
});

confirmPasswordField.addEventListener('input', (event) => {
  const isValid = validatePasswordsMatching() && confirmPasswordField.checkValidity();

  if (isValid) {
    setFieldValidStyle(confirmPasswordField);
  } else {
    setFieldInvalidStyle(confirmPasswordField);
  }
});

emailField.addEventListener('input', () => {
  const isValid = emailField.checkValidity();

  if (isValid) {
    setFieldValidStyle(emailField);
  } else {
    setFieldInvalidStyle(emailField);
  }
});

usernameField.addEventListener('invalid', (event) => {
  usernameInvalidTooltip.innerHTML = event.target.validationMessage;
});

passwordField.addEventListener('invalid', (event) => {
  passwordInvalidTooltip.innerHTML = event.target.validationMessage;
});

confirmPasswordField.addEventListener('invalid', (event) => {
  confirmPasswordInvalidTooltip.innerHTML = event.target.validationMessage;
});

emailField.addEventListener('invalid', (event) => {
  emailInvalidTooltip.innerHTML = event.target.validationMessage;
});

form.addEventListener('input', () => {
  const isValid = form.checkValidity();

  if (isValid) {
    submitBtn.disabled = false;
  } else {
    submitBtn.disabled = true;
  }
});

function setFieldInvalidStyle(field) {
  field.classList.add('is-invalid');
  field.classList.remove('is-valid');
}

function setFieldValidStyle(field) {
  field.classList.add('is-valid');
  field.classList.remove('is-invalid');
}

function validatePasswordsMatching() {
  if (passwordField.value === confirmPasswordField.value) {
    confirmPasswordField.setCustomValidity('');
    return true;
  }

  confirmPasswordField.setCustomValidity("Passwords don't match");
  return false;
}
