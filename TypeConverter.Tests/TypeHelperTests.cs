﻿using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;


using TypeConverter.Extensions;
using TypeConverter.Tests.Stubs;
using TypeConverter.Tests.Utils;
using TypeConverter.Utils;

using Xunit;
using Xunit.Abstractions;

namespace TypeConverter.Tests
{
    public class TypeHelperTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TypeHelperTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        ////[Fact]
        ////public void ShouldIsImplicitlyCastableTo()
        ////{
        ////    CastTestRunner.RunTests((from, to) => from.IsImplicitlyCastableTo(to), castFlag: true);
        ////}

        [Fact]
        public void ShouldRunAllImplicitCasts()
        {
            TypeHelper.IsCacheEnabled = false;
            CastTestRunner.CastFlag castFlag = CastTestRunner.CastFlag.Implicit;

            CastTestRunner.RunTests((testCase) =>
                {
                    // Arrange
                    var value = CastTestRunner.GenerateValueForType(testCase.SourceType);
                    var generatedTestSuccessful = CastTestRunner.CastValueWithGeneratedCode(value, testCase.SourceType, testCase.TargetType, castFlag);

                    // Act
                    var castResult = TypeHelper.CastImplicitlyTo(value, testCase.TargetType);

                    // Assert
                    var isSuccessful = CastTestRunner.AreEqual(
                        this.testOutputHelper,
                        testCase.SourceType,
                        testCase.TargetType,
                        generatedTestSuccessful,
                        castResult,
                        castFlag);

                    return isSuccessful;
                }, castFlag: castFlag);
        }

        ////[Fact]
        ////public void ShouldIsCastableTo()
        ////{
        ////    CastTestRunner.RunTests((from, to) => from.IsCastableTo(to), castFlag: false);
        ////}

        [Fact]
        public void ShouldRunAllExplicitCasts()
        {
            TypeHelper.IsCacheEnabled = false;
            CastTestRunner.CastFlag castFlag = CastTestRunner.CastFlag.Explicit;

            CastTestRunner.RunTests((testCase) =>
                {
                    // Arrange
                    var value = CastTestRunner.GenerateValueForType(testCase.SourceType);
                    var generatedTestSuccessful = CastTestRunner.CastValueWithGeneratedCode(value, testCase.SourceType, testCase.TargetType, castFlag);

                    // Act
                    var castResult = TypeHelper.CastTo(value, testCase.TargetType);

                    // Assert
                    var isSuccessful = CastTestRunner.AreEqual(
                        this.testOutputHelper, 
                        testCase.SourceType, 
                        testCase.TargetType,
                        generatedTestSuccessful, 
                        castResult, 
                        castFlag);

                    return isSuccessful;
                }, castFlag: castFlag);
        }

        [Fact]
        public void FactMethodName()
        {
            // Arrange
            Operators2 o1 = new Operators2();
            Operators2 o2 = new Operators2();

            // Act
            var objectEquals = Equals(o1, o2);
            var operatorsEquals = o1.Equals(o2);

            // Assert
            objectEquals.Should().BeTrue();
            operatorsEquals.Should().BeTrue();
        }

        [Fact]
        public void ShouldGenerateValueForEachConsideredType()
        {
            // Arrange
            var allTypesToConsider = CastTestRunner.GetAllTestTypes().ToList();
            var values = new List<object>(allTypesToConsider.Count);

            // Act
            foreach (var type in allTypesToConsider)
            {
                var value = CastTestRunner.GenerateValueForType(type);
                this.testOutputHelper.WriteLine("Type: {0}, Value: {1}", type.GetFormattedName(), value);
                values.Add(value);
            }

            // Assert
            values.Should().HaveCount(allTypesToConsider.Count);
        }

        [Fact]
        public void ShouldSwollowInvalidProgramExceptionWhenNullableDecimalIsCastedToOperators2()
        {
            // Arrange
            CastTestRunner.CastFlag castFlag = CastTestRunner.CastFlag.Explicit;
            decimal? obj = 1234m;

            // Act
            var castedValue = CastTestRunner.CastValueWithGeneratedCode(obj, typeof(Nullable<decimal>), typeof(Operators2), castFlag);

            // Assert
            castedValue.IsSuccessful.Should().BeTrue();
        }
    }
}