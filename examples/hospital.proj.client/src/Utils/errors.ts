// Define a mapping from error codes to user-friendly messages
const appErrors = {
    // Account-related errors
    'account.activation.code.is.invalid': 'The activation code is invalid.',
    'account.activation.code.has.expired': 'The activation code has expired.',
    'account.change.password.secret.is.invalid': 'The password change secret is invalid.',
    'account.change.password.secret.has.expired': 'The password change secret has expired.',
    'account.change.password.submit.has.expired': 'The password change request has expired.',
    'account.password.confirmation.failed': 'Password confirmation failed.',
    'account.firstName.is.too.large': 'The first name is too long.',
    'account.middleName.is.too.large': 'The middle name is too long.',
    'account.lastName.is.too.large': 'The last name is too long.',
    'account.firstName.is.required': 'The first name is required.',
    'account.middleName.is.required': 'The middle name is required.',
    'account.lastName.is.required': 'The last name is required.',
    'account.email.is.already.registered': 'The email is already registered.',
    'account.email.doesn\'t.exist': 'Email account unavailable or doesn\'t exist',
    'account.passwords.don\'t.match': 'The passwords do not match.',
  
    // User-related errors
    'user.not.found': 'The user was not found.',
  
    // General errors
    'record.not.found': 'The requested record was not found.',
    'value.is.invalid': 'The value provided is invalid.',
    'value.is.required': 'A value is required.',
    'invalid.string.length': 'The string length is invalid.',
    'collection.is.too.small': 'The collection is too small.',
    'collection.is.too.large': 'The collection is too large.',
    'string.is.too.small': 'The string is too small.',
    'string.is.too.large': 'The string is too large.',
    'internal.server.error': 'An internal server error occurred.',
    'request.body.is.null': 'The request body is null.',
    'route.value.is.null': 'The route value is null.',
  
    // Infrastructure-related errors
    'infrastructure.email.wasn\'t.sent': 'The email could not be sent.',
    'infrastructure.email.doesn\'t.exist.or.unavailable': 'The email address does not exist or is unavailable.',
    'infrastructure.failed.to.add.record': 'Failed to add the record.',
    'infrastructure.failed.to.save.changes': 'Failed to save changes.',
  };
  
  export { appErrors  };
  