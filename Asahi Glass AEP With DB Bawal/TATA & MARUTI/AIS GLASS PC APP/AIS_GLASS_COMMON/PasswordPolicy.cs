using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AIS_GLASS_COMMON
{
    public enum PasswordScore
    {
        Blank,
        VeryWeak,
        Weak,
        Medium,
        Strong,
        VeryStrong
    }

    public class PasswordPolicy
    {
        public static PasswordScore CheckPasswordStrength(string password)
        {
            int N = 0;
            int L = password.Length;
            if (L == 0)
                return PasswordScore.Blank;
            if (Regex.IsMatch(password, @"[\d]", RegexOptions.ECMAScript))
                N += 10;
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
                N += 26;
            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                N += 26;
            if (Regex.IsMatch(password, @"[~`!@#$%\^\&\*\(\)\-_\+=\[\{\]\}\|\\;:'\""<\,>\.\?\/£]", RegexOptions.ECMAScript) && password.Length > 8)
                N += 33;
            int H = Convert.ToInt32(L * (Math.Round(Math.Log(N) / Math.Log(2))));
            if (H <= 32) return PasswordScore.VeryWeak;
            if (H <= 48) return PasswordScore.Weak;
            if (H <= 64) return PasswordScore.Medium;
            if (H <= 80) return PasswordScore.Strong;
            return PasswordScore.VeryStrong;
        }

    }
    

}
