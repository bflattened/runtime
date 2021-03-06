// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// (C) 2002 Ville Palo
// (C) 2003 Martin Willemoes Hansen

// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Xunit;
using System.Xml;
using System.Data.SqlTypes;

namespace System.Data.Tests.SqlTypes
{
    public class SqlDateTimeTest
    {
        private long[] _myTicks = {
            631501920000000000L,    // 25 Feb 2002 - 00:00:00
            631502475130080000L,    // 25 Feb 2002 - 15:25:13,8
            631502115130080000L,    // 25 Feb 2002 - 05:25:13,8
            631502115000000000L,    // 25 Feb 2002 - 05:25:00
            631502115130000000L,    // 25 Feb 2002 - 05:25:13
            631502079130000000L,    // 25 Feb 2002 - 04:25:13
            629197085770000000L     // 06 Nov 1994 - 08:49:37
        };

        private SqlDateTime _test1;
        private SqlDateTime _test2;
        private SqlDateTime _test3;

        public SqlDateTimeTest()
        {
            _test1 = new SqlDateTime(2002, 10, 19, 9, 40, 0);
            _test2 = new SqlDateTime(2003, 11, 20, 10, 50, 1);
            _test3 = new SqlDateTime(2003, 11, 20, 10, 50, 1);
        }

        // Test constructor
        [Fact]
        public void Create()
        {
            // SqlDateTime (DateTime)
            SqlDateTime cTest = new SqlDateTime(
                new DateTime(2002, 5, 19, 3, 34, 0));
            Assert.Equal(2002, cTest.Value.Year);

            // SqlDateTime (int, int)
            cTest = new SqlDateTime(0, 0);

            // SqlDateTime (int, int, int)
            Assert.Equal(1900, cTest.Value.Year);
            Assert.Equal(1, cTest.Value.Month);
            Assert.Equal(1, cTest.Value.Day);
            Assert.Equal(0, cTest.Value.Hour);

            // SqlDateTime (int, int, int, int, int, int)
            cTest = new SqlDateTime(5000, 12, 31);
            Assert.Equal(5000, cTest.Value.Year);
            Assert.Equal(12, cTest.Value.Month);
            Assert.Equal(31, cTest.Value.Day);

            // SqlDateTime (int, int, int, int, int, int, double)
            cTest = new SqlDateTime(1978, 5, 19, 3, 34, 0);
            Assert.Equal(1978, cTest.Value.Year);
            Assert.Equal(5, cTest.Value.Month);
            Assert.Equal(19, cTest.Value.Day);
            Assert.Equal(3, cTest.Value.Hour);
            Assert.Equal(34, cTest.Value.Minute);
            Assert.Equal(0, cTest.Value.Second);

            Assert.Throws<SqlTypeException>(() => new SqlDateTime(10000, 12, 31));

            // SqlDateTime (int, int, int, int, int, int, int)
            cTest = new SqlDateTime(1978, 5, 19, 3, 34, 0, 12);
            Assert.Equal(1978, cTest.Value.Year);
            Assert.Equal(5, cTest.Value.Month);
            Assert.Equal(19, cTest.Value.Day);
            Assert.Equal(3, cTest.Value.Hour);
            Assert.Equal(34, cTest.Value.Minute);
            Assert.Equal(0, cTest.Value.Second);
            Assert.Equal(0, cTest.Value.Millisecond);
        }

        // Test public fields
        [Fact]
        public void PublicFields()
        {
            // MaxValue
            Assert.Equal(9999, SqlDateTime.MaxValue.Value.Year);
            Assert.Equal(12, SqlDateTime.MaxValue.Value.Month);
            Assert.Equal(31, SqlDateTime.MaxValue.Value.Day);
            Assert.Equal(23, SqlDateTime.MaxValue.Value.Hour);
            Assert.Equal(59, SqlDateTime.MaxValue.Value.Minute);
            Assert.Equal(59, SqlDateTime.MaxValue.Value.Second);

            // MinValue
            Assert.Equal(1753, SqlDateTime.MinValue.Value.Year);
            Assert.Equal(1, SqlDateTime.MinValue.Value.Month);
            Assert.Equal(1, SqlDateTime.MinValue.Value.Day);
            Assert.Equal(0, SqlDateTime.MinValue.Value.Hour);
            Assert.Equal(0, SqlDateTime.MinValue.Value.Minute);
            Assert.Equal(0, SqlDateTime.MinValue.Value.Second);

            // Null
            Assert.True(SqlDateTime.Null.IsNull);

            // SQLTicksPerHour
            Assert.Equal(1080000, SqlDateTime.SQLTicksPerHour);

            // SQLTicksPerMinute
            Assert.Equal(18000, SqlDateTime.SQLTicksPerMinute);

            // SQLTicksPerSecond
            Assert.Equal(300, SqlDateTime.SQLTicksPerSecond);
        }

        // Test properties
        [Fact]
        public void Properties()
        {
            // DayTicks
            Assert.Equal(37546, _test1.DayTicks);

            Assert.Throws<SqlNullValueException>(() => SqlDateTime.Null.DayTicks);

            // IsNull
            Assert.True(SqlDateTime.Null.IsNull);
            Assert.False(_test2.IsNull);

            // TimeTicks
            Assert.Equal(10440000, _test1.TimeTicks);

            Assert.Throws<SqlNullValueException>(() => SqlDateTime.Null.TimeTicks);

            // Value
            Assert.Equal(2003, _test2.Value.Year);
            Assert.Equal(2002, _test1.Value.Year);
        }

        // PUBLIC METHODS

        [Fact]
        public void CompareTo()
        {
            SqlString testString = new SqlString("This is a test");

            Assert.True(_test1.CompareTo(_test3) < 0);
            Assert.True(_test2.CompareTo(_test1) > 0);
            Assert.Equal(0, _test2.CompareTo(_test3));
            Assert.True(_test1.CompareTo(SqlDateTime.Null) > 0);

            Assert.Throws<ArgumentException>(() => _test1.CompareTo(testString));
        }

        [Fact]
        public void EqualsMethods()
        {
            Assert.False(_test1.Equals(_test2));
            Assert.False(_test1.Equals((object)_test2));
            Assert.False(_test2.Equals(new SqlString("TEST")));
            Assert.True(_test2.Equals(_test3));
            Assert.True(_test2.Equals((object)_test3));

            // Static Equals()-method
            Assert.True(SqlDateTime.Equals(_test2, _test3).Value);
            Assert.False(SqlDateTime.Equals(_test1, _test2).Value);
        }

        [Fact]
        public void GetHashCodeTest()
        {
            // FIXME: Better way to test HashCode
            Assert.Equal(_test1.GetHashCode(), _test1.GetHashCode());
            Assert.NotEqual(_test2.GetHashCode(), _test1.GetHashCode());
        }

        [Fact]
        public void Greaters()
        {
            // GreateThan ()
            Assert.False(SqlDateTime.GreaterThan(_test1, _test2).Value);
            Assert.True(SqlDateTime.GreaterThan(_test2, _test1).Value);
            Assert.False(SqlDateTime.GreaterThan(_test2, _test3).Value);

            // GreaterTharOrEqual ()
            Assert.False(SqlDateTime.GreaterThanOrEqual(_test1, _test2).Value);
            Assert.True(SqlDateTime.GreaterThanOrEqual(_test2, _test1).Value);
            Assert.True(SqlDateTime.GreaterThanOrEqual(_test2, _test3).Value);
        }

        [Fact]
        public void Lessers()
        {
            // LessThan()
            Assert.False(SqlDateTime.LessThan(_test2, _test3).Value);
            Assert.False(SqlDateTime.LessThan(_test2, _test1).Value);
            Assert.True(SqlDateTime.LessThan(_test1, _test3).Value);

            // LessThanOrEqual ()
            Assert.True(SqlDateTime.LessThanOrEqual(_test1, _test2).Value);
            Assert.False(SqlDateTime.LessThanOrEqual(_test2, _test1).Value);
            Assert.True(SqlDateTime.LessThanOrEqual(_test3, _test2).Value);
            Assert.True(SqlDateTime.LessThanOrEqual(_test1, SqlDateTime.Null).IsNull);
        }

        [Fact]
        public void NotEquals()
        {
            Assert.True(SqlDateTime.NotEquals(_test1, _test2).Value);
            Assert.True(SqlDateTime.NotEquals(_test3, _test1).Value);
            Assert.False(SqlDateTime.NotEquals(_test2, _test3).Value);
            Assert.True(SqlDateTime.NotEquals(SqlDateTime.Null, _test2).IsNull);
        }

        [Fact]
        public void Parse()
        {
            Assert.Throws<ArgumentNullException>(() => SqlDateTime.Parse(null));

            Assert.Throws<FormatException>(() => SqlDateTime.Parse("not-a-number"));

            SqlDateTime t1 = SqlDateTime.Parse("02/25/2002");
            Assert.Equal(_myTicks[0], t1.Value.Ticks);

            // Thanks for Martin Baulig for these (DateTimeTest.cs)
            t1 = SqlDateTime.Parse("2002-02-25");
            Assert.Equal(_myTicks[0], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("Monday, 25 February 2002");
            Assert.Equal(_myTicks[0], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("Monday, 25 February 2002 05:25");
            Assert.Equal(_myTicks[3], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("Monday, 25 February 2002 05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("02/25/2002 05:25");
            Assert.Equal(_myTicks[3], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("02/25/2002 05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);
            t1 = SqlDateTime.Parse("2002-02-25 04:25:13Z");
            t1 = t1.Value.ToUniversalTime();
            Assert.Equal(2002, t1.Value.Year);
            Assert.Equal(02, t1.Value.Month);
            Assert.Equal(25, t1.Value.Day);
            Assert.Equal(04, t1.Value.Hour);
            Assert.Equal(25, t1.Value.Minute);
            Assert.Equal(13, t1.Value.Second);

            SqlDateTime t2 = new SqlDateTime(DateTime.Today.Year, 2, 25);
            t1 = SqlDateTime.Parse("February 25");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(DateTime.Today.Year, 2, 8);
            t1 = SqlDateTime.Parse("February 08");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t1 = SqlDateTime.Parse("Mon, 25 Feb 2002 04:25:13 GMT");
            t1 = t1.Value.ToUniversalTime();
            Assert.Equal(2002, t1.Value.Year);
            Assert.Equal(02, t1.Value.Month);
            Assert.Equal(25, t1.Value.Day);
            Assert.Equal(04, t1.Value.Hour);
            Assert.Equal(25, t1.Value.Minute);
            Assert.Equal(13, t1.Value.Second);

            t1 = SqlDateTime.Parse("2002-02-25T05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);

            t2 = DateTime.Today + new TimeSpan(5, 25, 0);
            t1 = SqlDateTime.Parse("05:25");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = DateTime.Today + new TimeSpan(5, 25, 13);
            t1 = SqlDateTime.Parse("05:25:13");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(2002, 2, 1);
            t1 = SqlDateTime.Parse("2002 February");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(2002, 2, 1);
            t1 = SqlDateTime.Parse("2002 February");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(DateTime.Today.Year, 2, 8);
            t1 = SqlDateTime.Parse("February 8");

            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);
        }

        // OPERATORS
        [Fact]
        public void ArithmeticOperators()
        {
            TimeSpan testSpan = new TimeSpan(20, 1, 20, 20);
            SqlDateTime resultDateTime;

            // "+"-operator
            resultDateTime = _test1 + testSpan;
            Assert.Equal(2002, resultDateTime.Value.Year);
            Assert.Equal(8, resultDateTime.Value.Day);
            Assert.Equal(11, resultDateTime.Value.Hour);
            Assert.Equal(0, resultDateTime.Value.Minute);
            Assert.Equal(20, resultDateTime.Value.Second);
            Assert.True((SqlDateTime.Null + testSpan).IsNull);

            Assert.Throws<ArgumentOutOfRangeException>(() => SqlDateTime.MaxValue + testSpan);

            // "-"-operator
            resultDateTime = _test1 - testSpan;
            Assert.Equal(2002, resultDateTime.Value.Year);
            Assert.Equal(29, resultDateTime.Value.Day);
            Assert.Equal(8, resultDateTime.Value.Hour);
            Assert.Equal(19, resultDateTime.Value.Minute);
            Assert.Equal(40, resultDateTime.Value.Second);
            Assert.True((SqlDateTime.Null - testSpan).IsNull);

            Assert.Throws<SqlTypeException>(() => SqlDateTime.MinValue - testSpan);
        }

        [Fact]
        public void ThanOrEqualOperators()
        {
            // == -operator
            Assert.True((_test2 == _test3).Value);
            Assert.False((_test1 == _test2).Value);
            Assert.True((_test1 == SqlDateTime.Null).IsNull);

            // != -operator
            Assert.False((_test2 != _test3).Value);
            Assert.True((_test1 != _test3).Value);
            Assert.True((_test1 != SqlDateTime.Null).IsNull);

            // > -operator
            Assert.True((_test2 > _test1).Value);
            Assert.False((_test3 > _test2).Value);
            Assert.True((_test1 > SqlDateTime.Null).IsNull);

            // >=  -operator
            Assert.False((_test1 >= _test3).Value);
            Assert.True((_test3 >= _test1).Value);
            Assert.True((_test2 >= _test3).Value);
            Assert.True((_test1 >= SqlDateTime.Null).IsNull);

            // < -operator
            Assert.False((_test2 < _test1).Value);
            Assert.True((_test1 < _test3).Value);
            Assert.False((_test2 < _test3).Value);
            Assert.True((_test1 < SqlDateTime.Null).IsNull);

            // <= -operator
            Assert.True((_test1 <= _test3).Value);
            Assert.False((_test3 <= _test1).Value);
            Assert.True((_test2 <= _test3).Value);
            Assert.True((_test1 <= SqlDateTime.Null).IsNull);
        }

        [Fact]
        public void SqlDateTimeToDateTime()
        {
            Assert.Equal(2002, ((DateTime)_test1).Year);
            Assert.Equal(2003, ((DateTime)_test2).Year);
            Assert.Equal(10, ((DateTime)_test1).Month);
            Assert.Equal(19, ((DateTime)_test1).Day);
            Assert.Equal(9, ((DateTime)_test1).Hour);
            Assert.Equal(40, ((DateTime)_test1).Minute);
            Assert.Equal(0, ((DateTime)_test1).Second);
        }

        [Fact]
        public void SqlStringToSqlDateTime()
        {
            SqlString testString = new SqlString("02/25/2002");
            SqlDateTime t1 = (SqlDateTime)testString;

            Assert.Equal(_myTicks[0], t1.Value.Ticks);

            // Thanks for Martin Baulig for these (DateTimeTest.cs)
            Assert.Equal(_myTicks[0], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("Monday, 25 February 2002");
            Assert.Equal(_myTicks[0], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("Monday, 25 February 2002 05:25");
            Assert.Equal(_myTicks[3], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("Monday, 25 February 2002 05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("02/25/2002 05:25");
            Assert.Equal(_myTicks[3], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("02/25/2002 05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);
            t1 = (SqlDateTime)new SqlString("2002-02-25 04:25:13Z");
            t1 = t1.Value.ToUniversalTime();
            Assert.Equal(2002, t1.Value.Year);
            Assert.Equal(02, t1.Value.Month);
            Assert.Equal(25, t1.Value.Day);
            Assert.Equal(04, t1.Value.Hour);
            Assert.Equal(25, t1.Value.Minute);
            Assert.Equal(13, t1.Value.Second);

            SqlDateTime t2 = new SqlDateTime(DateTime.Today.Year, 2, 25);
            t1 = (SqlDateTime)new SqlString("February 25");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(DateTime.Today.Year, 2, 8);
            t1 = (SqlDateTime)new SqlString("February 08");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t1 = (SqlDateTime)new SqlString("Mon, 25 Feb 2002 04:25:13 GMT");
            t1 = t1.Value.ToUniversalTime();
            Assert.Equal(2002, t1.Value.Year);
            Assert.Equal(02, t1.Value.Month);
            Assert.Equal(25, t1.Value.Day);
            Assert.Equal(04, t1.Value.Hour);
            Assert.Equal(25, t1.Value.Minute);
            Assert.Equal(13, t1.Value.Second);

            t1 = (SqlDateTime)new SqlString("2002-02-25T05:25:13");
            Assert.Equal(_myTicks[4], t1.Value.Ticks);

            t2 = DateTime.Today + new TimeSpan(5, 25, 0);
            t1 = (SqlDateTime)new SqlString("05:25");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = DateTime.Today + new TimeSpan(5, 25, 13);
            t1 = (SqlDateTime)new SqlString("05:25:13");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(2002, 2, 1);
            t1 = (SqlDateTime)new SqlString("2002 February");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(2002, 2, 1);
            t1 = (SqlDateTime)new SqlString("2002 February");
            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);

            t2 = new SqlDateTime(DateTime.Today.Year, 2, 8);
            t1 = (SqlDateTime)new SqlString("February 8");

            Assert.Equal(t2.Value.Ticks, t1.Value.Ticks);
        }

        [Fact]
        public void DateTimeToSqlDateTime()
        {
            DateTime dateTimeTest = new DateTime(2002, 10, 19, 11, 53, 4);
            SqlDateTime result = dateTimeTest;
            Assert.Equal(2002, result.Value.Year);
            Assert.Equal(10, result.Value.Month);
            Assert.Equal(19, result.Value.Day);
            Assert.Equal(11, result.Value.Hour);
            Assert.Equal(53, result.Value.Minute);
            Assert.Equal(4, result.Value.Second);
        }

        [Fact]
        public void TicksRoundTrip()
        {
            SqlDateTime d1 = new SqlDateTime(2007, 05, 04, 18, 02, 40, 398.25);
            SqlDateTime d2 = new SqlDateTime(d1.DayTicks, d1.TimeTicks);

            Assert.Equal(39204, d1.DayTicks);
            Assert.Equal(19488119, d1.TimeTicks);
            Assert.Equal(633138985603970000, d1.Value.Ticks);
            Assert.Equal(d1.DayTicks, d2.DayTicks);
            Assert.Equal(d1.TimeTicks, d2.TimeTicks);
            Assert.Equal(d1.Value.Ticks, d2.Value.Ticks);
            Assert.Equal(d1, d2);
        }

        [Fact]
        public void EffingBilisecond()
        {
            SqlDateTime d1 = new SqlDateTime(2007, 05, 04, 18, 02, 40, 398252);

            Assert.Equal(39204, d1.DayTicks);
            Assert.Equal(19488119, d1.TimeTicks);
            Assert.Equal(633138985603970000, d1.Value.Ticks);
        }

        [Fact]
        public void GetXsdTypeTest()
        {
            XmlQualifiedName qualifiedName = SqlDateTime.GetXsdType(null);
            Assert.Equal("dateTime", qualifiedName.Name);
        }
    }
}
