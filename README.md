# DimensionKeeper

To load your location you need to override the classes located in the DimensionService/Configuration folder.

All public code has documentation. Below is some general helpful info.

#### Dimension 
- Allows you to store specify data of your dimension. 
- It already has some standard data about Tiles, Chast etc.
#### DimensionStorage 
- Allows you to manage Dimension storage. (File, TagCompaund etc). See useful implementations in HelperImplementations/Storages
#### DimensionInjector 
- Allows you to add the DimensionPhases. See useful implementations in HelperImplementations/Injectors
#### DimensionPhase 
- Allows you to manage what happen when dimention is loading/synс/clearing. See useful implementations in HelperImplementations/Phases

#### DimensionRegister 
- Allows you to register your dimension type. The type is combination of DimensionStorage, DimensionInjector and Dimension.
- Example: 
```C#
  DimensionRegister.Instance.Register<StandardInjector<Dimension>, TagCompoundFromFileStorage<Dimension>, Dimension>("TypeName");
```
- There is helpful the IDimensionRegister interface which allow you to structure the addition of types. Use the DimensionRegister.SetupDimensionTypesRegister<IDimensionRegisterImplementation>() method in the Load method of your mod.
```C#
  DimensionRegister.SetupDimensionTypesRegister<IDimensionRegisterImplementation>();
```
  
#### SingleEntryFactory 
- Allows you to get the named SingleEntryDimension. Use GetEntry method:
```C#
  SingleEntryFactory.GetEntry("EntryName")
```

#### SingleEntryDimension 
- The main class which allow you to Load the dimensions by its registred type with specify the id.
- **Dont forget to specify the LocationToLoad property before calling any methods of class.**
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#### EyeDropper
- Also there is usful tool for easy creation a location. To use it:
1. In the Load method of your mod set the EnableEyeDropper property of DimensionKeeperMod class to true.
```C#
  public override void Load()
  {
      ModContent.GetInstance<DimensionKeeperMod>().EnableEyeDropper = true;
  }
```

2. Register type with name DimensionKeeperMod.EyeDropperTypeName. (You can use classes from HelperImplementations folder. It will be save files to the Resource folder created in the tModLoader folder):
```C#
  var eyeDropperTypeName = ModContent.GetInstance<DimensionKeeperMod>().EyeDropperTypeName;
  register.Register<StandardInjector<Dimension>, FileTagCompoundStorage<Dimension>, Dimension>(eyeDropperTypeName);
```

3. Then use EyeDropperUpdater's static methods for Show ot Hide the EyeDropper tool.
```C#
  EyeDropperUpdater.Show();
```
