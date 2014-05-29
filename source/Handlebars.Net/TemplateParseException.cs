using System;

namespace Handlebars.Net {
	public class TemplateParseException : Exception {
		public string Token { get; set; }

		public TemplateParseException( string message, string token )
			: base( message ) {
			Token = token;
		}
	}
}
