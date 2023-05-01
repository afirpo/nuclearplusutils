using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.World.Entities;
using Mafi.Core.World.QuickTrade;

namespace NuclearPlusUtils
{
  public static partial class Recipes
  {
    public static readonly RecipeProto.ID SpentFuelFlareT1 = Ids.Recipes.CreateId("SpentFuelFlareT1");
    public static readonly RecipeProto.ID SpentFuelFlareT2 = Ids.Recipes.CreateId("SpentFuelFlareT2");
    public static readonly RecipeProto.ID SpentFuelFlareT3 = Ids.Recipes.CreateId("SpentFuelFlareT3");
    public static readonly RecipeProto.ID UranylNitrateFlare = Ids.Recipes.CreateId("UranylNitrateFlare");
    public static readonly RecipeProto.ID PlutoniumNitrateFlare = Ids.Recipes.CreateId("PlutoniumNitrateFlare");
  }

  public static partial class QuickTrades
  {
    public static readonly EntityProto.ID UraniumPelletsTrade = new EntityProto.ID("UraniumPelletsTrade");
    public static readonly EntityProto.ID PlutoniumPelletsTrade = new EntityProto.ID("PlutoniumPelletsTrade");
    public static readonly EntityProto.ID UraniumRodsTrade = new EntityProto.ID("UraniumRodsTrade");
    public static readonly EntityProto.ID PlutoniumRodsTrade = new EntityProto.ID("PlutoniumRodsTrade");
  }

  internal class SpentWasteData : IModData
  {
    public void RegisterData(ProtoRegistrator registrator)
    {
      registrator.RecipeProtoBuilder
         .Start(name: "Flare spent fuel solution 1"
           , recipeId: Recipes.SpentFuelFlareT1
           , machineId: Ids.Machines.Flare)
         .SetProductsDestroyReason(DestroyReason.Wasted)
         .EnablePartialExecution(1.Percent())
         .AddInput(12, NuclearPlus.NuclearPlusIds.Products.SpentFuelSolutionT1)
         .SetDuration(20.Seconds())
         .AddOutput(10, Ids.Products.PollutedAir, "VIRTUAL")
         .BuildAndAdd();

      registrator.RecipeProtoBuilder
         .Start(name: "Flare spent fuel solution 2"
           , recipeId: Recipes.SpentFuelFlareT2
           , machineId: Ids.Machines.Flare)
         .SetProductsDestroyReason(DestroyReason.Wasted)
         .EnablePartialExecution(1.Percent())
         .AddInput(12, NuclearPlus.NuclearPlusIds.Products.SpentFuelSolutionT2)
         .SetDuration(20.Seconds())
         .AddOutput(10, Ids.Products.PollutedAir, "VIRTUAL")
         .BuildAndAdd();

      registrator.RecipeProtoBuilder
         .Start(name: "Flare spent fuel solution 3"
           , recipeId: Recipes.SpentFuelFlareT3
           , machineId: Ids.Machines.Flare)
         .SetProductsDestroyReason(DestroyReason.Wasted)
         .EnablePartialExecution(1.Percent())
         .AddInput(12, NuclearPlus.NuclearPlusIds.Products.SpentFuelSolutionT3)
         .SetDuration(20.Seconds())
         .AddOutput(10, Ids.Products.PollutedAir, "VIRTUAL")
         .BuildAndAdd();

      registrator.RecipeProtoBuilder
         .Start(name: "Flare uranyl nitrate"
           , recipeId: Recipes.UranylNitrateFlare
           , machineId: Ids.Machines.Flare)
         .SetProductsDestroyReason(DestroyReason.Wasted)
         .EnablePartialExecution(1.Percent())
         .AddInput(12, NuclearPlus.NuclearPlusIds.Products.UranylNitrate)
         .SetDuration(20.Seconds())
         .AddOutput(6, Ids.Products.PollutedAir, "VIRTUAL")
         .BuildAndAdd();

      registrator.RecipeProtoBuilder
         .Start(name: "Flare plutonium nitrate"
           , recipeId: Recipes.PlutoniumNitrateFlare
           , machineId: Ids.Machines.Flare)
         .SetProductsDestroyReason(DestroyReason.Wasted)
         .EnablePartialExecution(1.Percent())
         .AddInput(12, NuclearPlus.NuclearPlusIds.Products.PlutoniumNitrate)
         .SetDuration(20.Seconds())
         .AddOutput(8, Ids.Products.PollutedAir, "VIRTUAL")
         .BuildAndAdd();

      var tradeVillage = registrator.PrototypesDb.GetOrThrow<WorldMapVillageProto>(Ids.World.Settlement1);

      Lyst<QuickTradePairProto> trades = new Lyst<QuickTradePairProto>(tradeVillage.QuickTrades.AsEnumerable())
      {
        new QuickTradePairProto(QuickTrades.UraniumPelletsTrade
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Gold), 10.Quantity())
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.UraniumPellets), 10.Quantity())
        , 0.5.Upoints()
        , 1
        , 1
        , 1
        , 0.Seconds()
        , 0.Percent()
        , 0.Percent()),

        new QuickTradePairProto(QuickTrades.UraniumRodsTrade
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Gold), 20.Quantity())
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.UraniumRod), 10.Quantity())
        , 0.5.Upoints()
        , 1
        , 1
        , 1
        , 0.Seconds()
        , 0.Percent()
        , 0.Percent()),

        new QuickTradePairProto(QuickTrades.PlutoniumPelletsTrade
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Gold), 20.Quantity())
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(NuclearPlus.NuclearPlusIds.Products.PlutoniumPellets), 10.Quantity())
        , 0.5.Upoints()
        , 1
        , 1
        , 1
        , 0.Seconds()
        , 0.Percent()
        , 0.Percent()),

        new QuickTradePairProto(QuickTrades.PlutoniumRodsTrade
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Gold), 30.Quantity())
        , new ProductQuantity(registrator.PrototypesDb.GetOrThrow<ProductProto>(NuclearPlus.NuclearPlusIds.Products.PlutoniumRod), 10.Quantity())
        , 0.5.Upoints()
        , 1
        , 1
        , 1
        , 0.Seconds()
        , 0.Percent()
        , 0.Percent())
      };

      tradeVillage.QuickTrades = ImmutableArray.CreateRange(trades);
    }
  }

  public sealed class NuclearPlusUtils : DataOnlyMod
  {
    public override string Name => "NuclearPlusUtils";

    public override int Version => 1;

    public NuclearPlusUtils(CoreMod coreMod, BaseMod baseMod, NuclearPlus.NuclearPlus nuclearMod)
    {
    }

    public override void RegisterPrototypes(ProtoRegistrator registrator)
    {
      registrator.RegisterData<SpentWasteData>();
    }
  }
}
