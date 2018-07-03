using System;
using Xamarin.Forms;

namespace FutureBall
{
    public class CommonAnswers
    {
        public static string[] options = {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes definitely",
            "You may rely on it",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Yes",
            "Signs point to yes",
            "Reply hazy try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",
            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful",
            "Computer says no",
            "Plan instead of 8-balling"
        };

        public static string GetAnswer()
        {
            var random = new Random();
            var index = random.Next(options.Length);
            return options[index];
        }
    }
}
