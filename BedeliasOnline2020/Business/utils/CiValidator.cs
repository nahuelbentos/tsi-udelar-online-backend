using System;
using System.Collections.Generic;

namespace Business.utils
{
  public class CiValidator
  {


    public CiValidator()
    {

    }

    private static int validation_digit(string ci)
    {
      var a = 0;
      var i = 0;
      if (ci.Length <= 6)
      {
        for (i = ci.Length; i < 7; i++)
        {
          ci = '0' + ci;
        }
      }
      for (i = 0; i < 7; i++)
      {
        a += (Int32.Parse("2987634"[i].ToString()) * Int32.Parse(ci[i].ToString())) % 10;
      }
      if (a % 10 == 0)
      {
        return 0;
      }
      else
      {
        return 10 - a % 10;
      }
    }

    public static bool Validate(string ci)
    {
      var dig = ci[ci.Length - 1];
      ci = ci.Substring(0, ci.Length - 1);

      int validDigitCalculated = validation_digit(ci);
      return (Int32.Parse(dig.ToString()) == validDigitCalculated);
    }

    public static bool ciInCollectionValids(string ci)
    {
      var validCI = new List<string>();
      var init = 11111111;
      for (int i = 1; i < 9; i++)
      {
        init *= i;
        validCI.Add(init.ToString());
      };

      return validCI.Contains(ci);
    }





  }
}