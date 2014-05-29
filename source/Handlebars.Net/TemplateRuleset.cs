namespace Handlebars.Net {
	public class TemplateRuleset {
		public string TokenOpen { get; protected set; }
		public string TokenClose { get; protected set; }
		public string TokenHelperOpen { get; protected set; }
		public string TokenHelperClose { get; protected set; }

		public static TemplateRuleset Handlebars = new TemplateRuleset {
			TokenOpen = "{{",
			TokenClose = "}}",
			TokenHelperOpen = "{{#",
			TokenHelperClose = "{{/"
		};
	}
}
