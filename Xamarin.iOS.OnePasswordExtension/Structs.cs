using System;
using Foundation;

namespace Xamarin.OnePassword
{
	public static class AppExtension
	{
		// Login Dictionary keys
		public static readonly NSString URLStringKey = (NSString)"url_string";
		public static readonly NSString UsernameKey = (NSString)"username";
		public static readonly NSString PasswordKey = (NSString)"password";
		public static readonly NSString TOTPKey = (NSString)"totp";
		public static readonly NSString TitleKey = (NSString)"login_title";
		public static readonly NSString NotesKey = (NSString)"notes";
		public static readonly NSString SectionTitleKey = (NSString)"section_title";
		public static readonly NSString FieldsKey = (NSString)"fields";
		public static readonly NSString ReturnedFieldsKey = (NSString)"returned_fields";
		public static readonly NSString OldPasswordKey = (NSString)"old_password";
		public static readonly NSString PasswordGereratorOptionsKey = (NSString)"password_generator_options";

		// Password Generator options
		public static readonly NSString GeneratedPasswordMinLengthKey = (NSString)"password_min_length";
		public static readonly NSString GeneratedPasswordMaxLengthKey = (NSString)"password_max_length";
		public static readonly NSString GeneratedPasswordRequireDigitsKey = (NSString)"password_require_digits";
		public static readonly NSString GeneratedPasswordRequireSymbolsKey = (NSString)"password_require_symbols";
		public static readonly NSString GeneratedPasswordForbiddenCharactersKey = (NSString)"password_forbidden_characters";

		// Errors
		public static readonly NSString ErrorDomain = (NSString)"OnePasswordExtension";

		public static readonly nint ErrorCodeCancelledByUser = 0;
		public static readonly nint ErrorCodeAPINotAvailable = 1;
		public static readonly nint ErrorCodeFailedToContactExtension = 2;
		public static readonly nint ErrorCodeFailedToLoadItemProviderData = 3;
		public static readonly nint ErrorCodeCollectFieldsScriptFailed = 4;
		public static readonly nint ErrorCodeFillFieldsScriptFailed = 5;
		public static readonly nint ErrorCodeUnexpectedData = 6;
		public static readonly nint ErrorCodeFailedToObtainURLStringFromWebView = 7;
	}
}
