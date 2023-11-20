const usernameField = document.getElementById('Input_UserName');
const passwordField = document.getElementById('Input_Password');

const usernameInvalidTooltip = document.getElementById('username-invalid-tooltip');
const passwordInvalidTooltip = document.getElementById('password-invalid-tooltip');

const form = document.getElementById('login-form');
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

  if (isValid) {
    setFieldValidStyle(passwordField);
  } else {
    setFieldInvalidStyle(passwordField);
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

function setFieldInvalidStyle(field) {
  field.classList.add('is-invalid');
  field.classList.remove('is-valid');
}

function setFieldValidStyle(field) {
  field.classList.add('is-valid');
  field.classList.remove('is-invalid');
}
