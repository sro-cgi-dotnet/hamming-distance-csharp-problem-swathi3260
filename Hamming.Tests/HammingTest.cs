using System;
using Xunit;
using FluentAssertions;
using Hamming;

namespace Hamming.Tests
{
    public class HammingTest
    {
        [Fact]
        public void EmptyStrands()
        {
            Hamming.Distance("", "").Should().Be(0);
        }

        [Fact]
        public void IdenticalStrands()
        {
            Hamming.Distance("A", "A").Should().Be(0);
        }

        [Fact]
        public void LongIdenticalStrands()
        {
           Hamming.Distance("GGACTGA", "GGACTGA").Should().Be(0); 
        }

        [Fact]
        public void CompleteDistanceInSingleNucletoideStrands()
        {
            Hamming.Distance("A", "G").Should().Be(1);
        }

        [Fact]
        public void CompleteDistanceInSmallStrands()
        {
            Hamming.Distance("AG", "CT").Should().Be(2);
        }

        [Fact]
        public void SmallDistanceInSmallStrands()
        {
            Hamming.Distance("AT", "CT").Should().Be(1);
        }

        [Fact]
        public void SmallDistance()
        {
            Hamming.Distance("GGACG", "GGTCG").Should().Be(1);
        }

        [Fact]
        public void SmallDistanceInLongStrands()
        {
            Hamming.Distance("ACCAGGG", "ACTATGG").Should().Be(2);
        }

        [Fact]
        public void NonUniqueCharacterInFirstStrand()
        {
            Hamming.Distance("AAG", "AAA").Should().Be(1);
        }

        [Fact]
        public void NonUniqueCharacterInSecondStrand()
        {
            Hamming.Distance("AAA", "AAG").Should().Be(1);
        }

        [Fact]
        public void SameNucleotidesInDifferentPositions()
        {
            Hamming.Distance("TAG", "GAT").Should().Be(2);
        }

        [Fact]
        public void LargeDistance()
        {
            Hamming.Distance("GATACA", "GCATAA").Should().Be(4);
        }

        [Fact]
        public void LargeDistanceInOffByOneStrand()
        {
            Hamming.Distance("GGACGGATTCTG", "AGGACGGATTCT").Should().Be(9);
        }

        [Fact]
        public void DisallowFirstStrandLonger()
        {
            Action CalculateHammingDistance = () => Hamming.Distance("AATG", "AAA");
            CalculateHammingDistance.Should().Throw<ArgumentException>().WithMessage("Hamming Distance can only be calculated over strings of equal length");
        }

        [Fact]
        public void DisallowSecondStrandLonger()
        {
            Action CalculateHammingDistance = () => Hamming.Distance("ATA", "AGTG");
            CalculateHammingDistance.Should().Throw<ArgumentException>().WithMessage("Hamming Distance can only be calculated over strings of equal length");
        }

        [Fact]
        public void NullInFirstStrand()
        {
            Action CalculateHammingDistance = () =>  Hamming.Distance(null, "ATGG");
            CalculateHammingDistance.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("original");
        }

        [Fact]
        public void NullInSecondStrand()
        {
            Action CalculateHammingDistance = () =>  Hamming.Distance("ATGG", null);
            CalculateHammingDistance.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("current");
        }   
    }
}
