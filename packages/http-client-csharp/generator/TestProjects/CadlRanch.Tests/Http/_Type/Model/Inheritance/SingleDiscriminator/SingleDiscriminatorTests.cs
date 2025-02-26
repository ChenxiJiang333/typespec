// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Reflection;
using System.Threading.Tasks;
using _Type.Model.Inheritance.SingleDiscriminator;
using NUnit.Framework;

namespace TestProjects.CadlRanch.Tests.Http._Type.Model.Inheritance.SingleDiscriminator
{
    public class SingleDiscriminatorTests : CadlRanchTestBase
    {
        [CadlRanchTest]
        public Task GetModel() => Test(async (host) =>
        {
            var result = await new SingleDiscriminatorClient(host, null).GetModelAsync();
            Assert.IsTrue(result.Value is Sparrow);
            Assert.AreEqual(1, ((Sparrow)result.Value).Wingspan);
        });

        [CadlRanchTest]
        public Task PutModel() => Test(async (host) =>
        {
            var body = new Sparrow(1);
            var response = await new SingleDiscriminatorClient(host, null).PutModelAsync(body);
            Assert.AreEqual(204, response.GetRawResponse().Status);
        });

        [CadlRanchTest]
        public Task GetRecursiveModel() => Test(async (host) =>
        {
            var result = await new SingleDiscriminatorClient(host, null).GetRecursiveModelAsync();
            Assert.IsTrue(result.Value is Eagle);
            var eagle = (Eagle)result.Value;
            Assert.AreEqual(5, eagle.Wingspan);
            Assert.IsTrue(eagle.Partner is Goose);
            var goose = (Goose)eagle.Partner;
            Assert.AreEqual(2, goose.Wingspan);
            Assert.AreEqual(1, eagle.Friends.Count);
            Assert.IsTrue(eagle.Friends[0] is SeaGull);
            var seaGull = (SeaGull)eagle.Friends[0];
            Assert.AreEqual(2, seaGull.Wingspan);
            Assert.AreEqual(1, eagle.Hate.Count);
            Assert.IsTrue(eagle.Hate.ContainsKey("key3"));
            Assert.IsTrue(eagle.Hate["key3"] is Sparrow);
            var sparrow = (Sparrow)eagle.Hate["key3"];
            Assert.AreEqual(1, sparrow.Wingspan);
        });

        [CadlRanchTest]
        public Task PutRecursiveModel() => Test(async (host) =>
        {
            var body = new Eagle(5)
            {
                Friends = { new SeaGull(2) },
                Hate =
                {
                    ["key3"] = new Sparrow(1)
                },
                Partner = new Goose(2)
            };
            var response = await new SingleDiscriminatorClient(host, null).PutRecursiveModelAsync(body);
            Assert.AreEqual(204, response.GetRawResponse().Status);
        });

        [CadlRanchTest]
        public Task GetMissingDiscriminator() => Test(async (host) =>
        {
            var result = await new SingleDiscriminatorClient(host, null).GetMissingDiscriminatorAsync();
            Assert.IsTrue(result.Value is Bird);

            var unknownBirdType = typeof(Bird).Assembly.GetType("_Type.Model.Inheritance.SingleDiscriminator.UnknownBird");
            Assert.IsNotNull(unknownBirdType);
            Assert.AreEqual(unknownBirdType, result.Value.GetType());
            Assert.AreEqual(1, result.Value.Wingspan);

            var kindProperty = result.Value.GetType().GetProperty("Kind", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.AreEqual("unknown", kindProperty?.GetValue(result.Value));
        });

        [CadlRanchTest]
        public Task GetWrongDiscriminator() => Test(async (host) =>
        {
            var result = await new SingleDiscriminatorClient(host, null).GetWrongDiscriminatorAsync();
            Assert.IsTrue(result.Value is Bird);

            var unknownBirdType = typeof(Bird).Assembly.GetType("_Type.Model.Inheritance.SingleDiscriminator.UnknownBird");
            Assert.IsNotNull(unknownBirdType);
            Assert.AreEqual(unknownBirdType, result.Value.GetType());
            Assert.AreEqual(1, result.Value.Wingspan);

            var kindProperty = result.Value.GetType().GetProperty("Kind", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.AreEqual("wrongKind", kindProperty?.GetValue(result.Value));
        });

        [CadlRanchTest]
        public Task GetLegacyModel() => Test(async (host) =>
        {
            var result = await new SingleDiscriminatorClient(host, null).GetLegacyModelAsync();
            Assert.IsTrue(result.Value is TRex);
            Assert.AreEqual(20, ((TRex)result.Value).Size);
        });
    }
}
