public enum BirdState
{
    Appearing,
    Dragable,
    Attached,
    Flying,
    Stopped,
    Disappearing,
}

public enum SfxTypes
{
    Pull,
    Launch,
    Explosion,
    Dash,
    Split,
    GlassImpact,
    WoodImpact,
    MetalImpact,
    CoinHit,
    CoinCollect,
    PigImpact,
    PigDestroy,
    Open,
}

public enum BlockTypes
{
    Glass,
    Wood,
    Metal,
}

public enum GameState
{
    Ready,
    Playing,
    Ended,
}