using UnityEngine;
using System.Collections;

public static class Levels {

    public static string encoding = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static int CtoI(char c) {
        return encoding.IndexOf(c);
    }

    public static int CtoI(int c) {
        char character = (char) (c%encoding.Length);
        return encoding.IndexOf(character);
    }

    public static char ItoC(int i) {
        i = i % encoding.Length;
        if (i < 0) {
            i += encoding.Length;
        }
        return encoding[i];
    } 

    static char[] level01(char[] chars) {
        for (int i = 0; i < chars.Length; i++) {
            chars[i] = ItoC(CtoI(chars[i]) + 1);
        }
        return chars;
    }

    static char[] level02(char[] chars) {
        for (int i = 0; i < chars.Length; i++) {
            chars [i] = ItoC (CtoI (chars [i]) - 2);
            
        }
        return chars;
    }

    static char[] level03(char[] chars) {
        for (int i = 0; i < chars.Length; i++) {
            if (CtoI (chars [i]) % 2 == 0) {
                chars [i] = ItoC (CtoI (chars [i]) + 3);
            } else {
                chars [i] = ItoC (CtoI (chars [i]) + 2);
            }
        }
        return chars;
    }

    static char[] level04(char[] chars){
        System.Array.Reverse(chars);
        return chars;
    }

    static char[] level05(char[] chars) {
        for (int i = 0; i < chars.Length; i++) {
            chars [i] = ItoC (CtoI (chars [i]) + 1);
            if (chars [i] % 2 == 0) {
                chars [i] = ItoC (CtoI (chars [i]) / 2);
            }
        }
        return chars;
    }

    static char[] level06(char[] chars) {
        int[] ints = { 3, 1, 4, 1, 5, 9 };
        for (int i = 0; i < chars.Length; i++) {
            chars [i] = ItoC (CtoI (chars [i]) + ints [i]);
        }
        return chars;
    }

    static char[] level07(char[] chars){
        System.Array.Reverse (chars);
        for (int i = 0; i < chars.Length; i++) {
            chars [i] = ItoC (CtoI (chars [i]) + 2);
        }
        return chars;
    }

    static char[] level08(char[] chars) {
        int[] ints = { 1, 1, 3, 3, 5, 5 };
        for (int i = 0; i < chars.Length; i++) {
            if (i % 2 == 0){
                chars[i] = ItoC(CtoI(chars[i]) + ints[i]);
            }
            else if (i % 2 == 1){
                chars[i] = ItoC(CtoI(chars[i]) - ints[i]);
            }
        }
        return chars;
    }

    static char[] level09(char[] chars) {
        for (int i = 0; i < chars.Length; i++) {
            chars [i] = ItoC (CtoI (chars [i]) * 2);
        }
        return chars;
    }

    static char[] level10(char[] chars){
        int letter = CtoI(chars [0]);
        int letter1 = CtoI(chars [1]);
        int letter2 = CtoI(chars [2]);
        int letter3 = CtoI(chars [3]);
        int letter4 = CtoI(chars [4]);
        int letter5 = CtoI(chars [5]);
        chars [1] = ItoC(CtoI(chars [1]) - letter);
        chars [2] = ItoC(CtoI(chars [2]) - letter1);
        chars [3] = ItoC(CtoI(chars [3]) - letter2);
        chars [4] = ItoC(CtoI(chars [4]) - letter3);
        chars [5] = ItoC(CtoI(chars [5]) - letter4);
        chars [0] = ItoC(CtoI(chars [0]) - letter5);
        return chars;
    }

    static char[] level11(char[] chars){
        char letter = chars[1];
        chars[1] = chars[3];
        chars[2] = ItoC(CtoI(chars[2]) + 6);
        chars[3] = letter;
        letter = chars [4];
        chars[4] = chars[5];
        chars[5] = letter;
        chars [1] = ItoC (CtoI (chars [1]) - 4);
        return chars;
    }

    public static char[] encodelevel(int level, char[] chars) {
        if (level == 1)
            return level01 (chars);
        else if (level == 2)
            return level02 (chars);
        else if (level == 3)
            return level03 (chars);
        else if (level == 4)
            return level04 (chars);
        else if (level == 5)
            return level05 (chars);
        else if (level == 6)
            return level06 (chars);
        else if (level == 7)
            return level07 (chars);
        else if (level == 8)
            return level08 (chars);
        else if (level == 9)
            return level09 (chars);
        else if (level == 10)
            return level10 (chars);
        else if (level == 11)
            return level11 (chars);
        else
            return ("You won!!".ToCharArray());
    }

    public static string getHint(int level){
        if (level == 1)
            return "Don't be afraid to try all 0s";
        else if (level == 2)
            return "Try entering 333333";
        else if (level == 3)
            return "Even the evens are odd";
        else if (level == 4)
            return "Make it backwards";
        else if (level == 5)
            return "Odd numbers don't divide by 2";
        else if (level == 6)
            return "Who doesn't like Pi?";
        else if (level == 7)
            return "Think back again";
        else if (level == 8)
            return "The odds decrease for this one";
        else if (level == 9)
            return "Numbers are easy, letters are not";
        else if (level == 10)
            return "It's not different, or is it?";
        else if (level == 11)
            return "Make the switch";
        else
            return "You won!";
    }
}