using GameTemplate.Scripts.Systems.Audio;
using GameTemplate.Scripts.UI.Game;
using GameTemplate.Scripts.UI.Game.EscapeMenu;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Scripts.Scopes
{
    public class GameScope : GameStateScope
    {
        public override bool Persists => false;
        public override GameState ActiveState => GameState.Game;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterComponentInHierarchy<GameCanvasView>();
            builder.RegisterEntryPoint<GameCanvasController>();
            
            builder.RegisterEntryPoint<EscapeCanvasController>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<EscapeCanvasView>();
            
            // Register the MenuMusicStarter as an entry point
            // VContainer will instantiate this class and call its Start() method.
            builder.RegisterEntryPoint<GameMusicStarter>();
        }
    }
}