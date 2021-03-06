﻿using FluentAssertions;
using Leprechaun.MetadataGeneration;
using Xunit;

namespace Leprechaun.Tests.MetadataGeneration
{
	public class StandardTypeNameGeneratorTests
	{
		[Theory,
			InlineData("/Foo", "/Foo/Bar", "Bar"),
			InlineData("/Foo/Bar", "/Foo/Bar/Baz/Quux", "Baz.Quux"),
			InlineData("/Foo", "/Foo/Name Transform.Test", "NameTransform.Test"),
			InlineData("/Foo", "/Foo/Name Transform", "NameTransform")]
		public void GetFullTypeName_ShouldPerformAsExpected(string rootNamespace, string fullPath, string expected)
		{
			var sut = new StandardTypeNameGenerator(rootNamespace);

			sut.GetFullTypeName(fullPath).Should().Be(expected);
		}

		[Theory, 
			InlineData("Foo", "Foo"),
			InlineData("9Foo9", "_9Foo9"), // identifier cannot begin with number
			InlineData("Field Name", "FieldName"),
			InlineData("field_name", "FieldName"),
			InlineData("Field.Name", "Field.Name")]
		public void ConvertToIdentifier_ShouldPerformAsExpected(string input, string expected)
		{
			var sut = new StandardTypeNameGenerator(string.Empty);

			sut.ConvertToIdentifier(input).Should().Be(expected);
		}
	}
}
