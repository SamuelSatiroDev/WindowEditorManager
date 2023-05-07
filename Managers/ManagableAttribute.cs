using System;

/// <summary>
/// An attribute to mark monobehaviors so they
/// can be imported into the MB manager editor window
/// </summary>
public class ManageableMonoAttribute : Attribute {}

/// <summary>
/// An attribute to mark scriptable objects so they
/// can be imported into the data manager editor window
/// </summary>
/// 
public class ManageableDataAttribute : Attribute { }