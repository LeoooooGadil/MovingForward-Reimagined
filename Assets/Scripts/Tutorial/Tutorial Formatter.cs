using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TutorialFormatter
{
	public static string Format(string input, Func<string, string> placeholderResolver) {
        if (input == null) {
            throw new ArgumentNullException(nameof(input));
        }

        if (placeholderResolver == null) {
            throw new ArgumentNullException(nameof(placeholderResolver));
        }

        return string.Join("", Parse(input, "{{", "}}")
            .Select(token => token.StartsWith("{{") && token.EndsWith("}}")
                ? ResolvePlaceholder(token, placeholderResolver)
                : token
            )
        );
    }

    private static string ResolvePlaceholder(string token, Func<string, string> placeholderResolver) {
        var key = token.Substring(2, token.Length - 4);
        var value = placeholderResolver(key);
        return value != null ? value : token;
    }

    private static IEnumerable<string> Parse(string input, string start, string end) {
        var currentIndex = 0;
        var startIndex = 0;
        while ((startIndex = input.IndexOf(start, currentIndex)) != -1) {
            yield return input.Substring(currentIndex, startIndex - currentIndex);
            var endIndex = input.IndexOf(end, startIndex + start.Length);
            if (endIndex == -1) {
                throw new ArgumentException($"No matching end token found for start token at index {startIndex}.");
            }

            yield return input.Substring(startIndex, endIndex - startIndex + end.Length);
            currentIndex = endIndex + end.Length;
        }

        yield return input.Substring(currentIndex);
    }
}
