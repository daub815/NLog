// 
// Copyright (c) 2004-2016 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System.Linq;

namespace NLog.UnitTests.Contexts
{
    using System;
#if NET4_0 || NET4_5
    using System.Threading.Tasks;
    using Xunit;

    public class MappedDiagnosticsLogicalContextTests
    {
        public MappedDiagnosticsLogicalContextTests()
        {
            MappedDiagnosticsLogicalContext.Clear();
        }

        [Fact]
        public void given_item_exists_when_getting_item_should_return_item_for_objecttype_2()
        {
            string key = "testKey1";
            object value = 5;

            MappedDiagnosticsLogicalContext.Set(key, value);

            string expected = "5";
            string actual = MappedDiagnosticsLogicalContext.Get(key);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void given_item_exists_when_getting_item_should_return_item_for_objecttype()
        {
            string key = "testKey2";
            object value = DateTime.Now;

            MappedDiagnosticsLogicalContext.Set(key, value);

            object actual = MappedDiagnosticsLogicalContext.GetObject(key);
            Assert.Equal(value, actual);
        }

        [Fact]
        public void given_no_item_exists_when_getting_item_should_return_null()
        {
            Assert.Null(MappedDiagnosticsLogicalContext.GetObject("itemThatShouldNotExist"));
        }

        [Fact]
        public void given_no_item_exists_when_getting_item_should_return_empty_string()
        {
            Assert.Empty(MappedDiagnosticsLogicalContext.Get("itemThatShouldNotExist"));
        }

        [Fact]
        public void given_item_exists_when_getting_item_should_return_item()
        {
            const string key = "Key";
            const string item = "Item";
            MappedDiagnosticsLogicalContext.Set(key, item);

            Assert.Equal(item, MappedDiagnosticsLogicalContext.Get(key));
        }

        [Fact]
        public void given_item_does_not_exist_when_setting_item_should_contain_item()
        {
            const string key = "Key";
            const string item = "Item";

            MappedDiagnosticsLogicalContext.Set(key, item);

            Assert.True(MappedDiagnosticsLogicalContext.Contains(key));
        }

        [Fact]
        public void given_item_exists_when_setting_item_should_not_throw()
        {
            const string key = "Key";
            const string item = "Item";
            MappedDiagnosticsLogicalContext.Set(key, item);

            Assert.DoesNotThrow(() => MappedDiagnosticsLogicalContext.Set(key, item));
        }

        [Fact]
        public void given_item_exists_when_setting_item_should_update_item()
        {
            const string key = "Key";
            const string item = "Item";
            const string newItem = "NewItem";
            MappedDiagnosticsLogicalContext.Set(key, item);

            MappedDiagnosticsLogicalContext.Set(key, newItem);

            Assert.Equal(newItem, MappedDiagnosticsLogicalContext.Get(key));
        }

        [Fact]
        public void given_no_item_exists_when_getting_items_should_return_empty_collection()
        {
            Assert.Equal(0,MappedDiagnosticsLogicalContext.GetNames().Count);
        }

        [Fact]
        public void given_item_exists_when_getting_items_should_return_that_item()
        {
            const string key = "Key";
            MappedDiagnosticsLogicalContext.Set(key, "Item");

            Assert.Equal(1, MappedDiagnosticsLogicalContext.GetNames().Count);
            Assert.True(MappedDiagnosticsLogicalContext.GetNames().Contains("Key"));

        }

        [Fact]
        public void given_item_exists_after_removing_item_when_getting_items_should_not_contain_item()
        {
            const string keyThatRemains1 = "Key1";
            const string keyThatRemains2 = "Key2";
            const string keyThatIsRemoved = "KeyR";

            MappedDiagnosticsLogicalContext.Set(keyThatRemains1, "7");
            MappedDiagnosticsLogicalContext.Set(keyThatIsRemoved, 7);
            MappedDiagnosticsLogicalContext.Set(keyThatRemains2, 8);

            MappedDiagnosticsLogicalContext.Remove(keyThatIsRemoved);

            Assert.Equal(2, MappedDiagnosticsLogicalContext.GetNames().Count);
            Assert.False(MappedDiagnosticsLogicalContext.GetNames().Contains(keyThatIsRemoved));
        }

        [Fact]
        public void given_item_does_not_exist_when_checking_if_context_contains_should_return_false()
        {
            Assert.False(MappedDiagnosticsLogicalContext.Contains("keyForItemThatDoesNotExist"));
        }

        [Fact]
        public void given_item_exists_when_checking_if_context_contains_should_return_true()
        {
            const string key = "Key";
            MappedDiagnosticsLogicalContext.Set(key, "Item");

            Assert.True(MappedDiagnosticsLogicalContext.Contains(key));

        }

        [Fact]
        public void given_item_exists_when_removing_item_should_not_contain_item()
        {
            const string keyForItemThatShouldExist = "Key";
            const string itemThatShouldExist = "Item";
            MappedDiagnosticsLogicalContext.Set(keyForItemThatShouldExist, itemThatShouldExist);

            MappedDiagnosticsLogicalContext.Remove(keyForItemThatShouldExist);

            Assert.False(MappedDiagnosticsLogicalContext.Contains(keyForItemThatShouldExist));
        }

        [Fact]
        public void given_item_does_not_exist_when_removing_item_should_not_throw()
        {
            const string keyForItemThatShouldExist = "Key";
            Assert.DoesNotThrow(() => MappedDiagnosticsLogicalContext.Remove(keyForItemThatShouldExist));
        }

        [Fact]
        public void given_item_does_not_exist_when_clearing_should_not_throw()
        {
            Assert.DoesNotThrow(MappedDiagnosticsLogicalContext.Clear);
        }

        [Fact]
        public void given_item_exists_when_clearing_should_not_contain_item()
        {
            const string key = "Key";
            MappedDiagnosticsLogicalContext.Set(key, "Item");

            MappedDiagnosticsLogicalContext.Clear();

            Assert.False(MappedDiagnosticsLogicalContext.Contains(key));
        }

        [Fact]
        public void given_multiple_threads_running_asynchronously_when_setting_and_getting_values_should_return_thread_specific_values()
        {
            const string key = "Key";
            const string valueForLogicalThread1 = "ValueForTask1";
            const string valueForLogicalThread2 = "ValueForTask2";
            const string valueForLogicalThread3 = "ValueForTask3";


            MappedDiagnosticsLogicalContext.Clear(true);

            var task1 = Task.Factory.StartNew(() => {
                MappedDiagnosticsLogicalContext.Set(key, valueForLogicalThread1);
                return MappedDiagnosticsLogicalContext.Get(key);
            });

            var task2 = Task.Factory.StartNew(() => {
                MappedDiagnosticsLogicalContext.Set(key, valueForLogicalThread2);
                return MappedDiagnosticsLogicalContext.Get(key);
            });

            var task3 = Task.Factory.StartNew(() => {
                MappedDiagnosticsLogicalContext.Set(key, valueForLogicalThread3);
                return MappedDiagnosticsLogicalContext.Get(key);
            });

            Task.WaitAll();

            Assert.Equal(task1.Result, valueForLogicalThread1);
            Assert.Equal(task2.Result, valueForLogicalThread2);
            Assert.Equal(task3.Result, valueForLogicalThread3);
        }
    }
#endif
}