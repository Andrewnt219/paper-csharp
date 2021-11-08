// <copyright file="TestUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Generator.Tests
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Newtonsoft.Json;

  public class TestUtils
  {
    public static void AreEqualByJson(object expected, object actual)
    {
      var expectedJson = JsonConvert.SerializeObject(expected);
      var actualJson = JsonConvert.SerializeObject(actual);
      Assert.AreEqual(expectedJson, actualJson);
    }
  }
}