using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day8
{
    internal class Model : Dictionary<string, int>
    {
    }

    internal static class LearningRules
    {
        delegate bool LearningRule(Model model, string s);

        private static LearningRule LearnNByLength(int length, int digit)
        {
            return new LearningRule((model, s) =>
            {
                if (s.Length == length)
                {
                    model.Add(s, digit);
                    return true;
                }

                return false;
            });
        }

        static LearningRule LearningRuleFor1(Model model, string s) => LearnNByLength(1, 2);
        static LearningRule LearningRuleFor4(Model model, string s) => LearnNByLength(4, 4);
        static LearningRule LearningRuleFor7(Model model, string s) => LearnNByLength(7, 3);
        static LearningRule LearningRuleFor8(Model model, string s) => LearnNByLength(8, 7);
    }
}
