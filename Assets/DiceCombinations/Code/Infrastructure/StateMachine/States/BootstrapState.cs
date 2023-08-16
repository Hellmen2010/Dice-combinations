using DiceCombinations.Code.Infrastructure.StateMachine.GameStateMachine;
using DiceCombinations.Code.Services.CoroutineRunner;
using DiceCombinations.Code.Services.DiceCombinationsCalculator;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Factories.GameFactory;
using DiceCombinations.Code.Services.Factories.UIFactory;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.SaveLoad;
using DiceCombinations.Code.Services.SceneLoader;
using DiceCombinations.Code.Services.Sound;
using DiceCombinations.Code.Services.StaticData;
using DiceCombinations.Code.Services.StaticData.StaticDataProvider;
using UnityEngine;

namespace DiceCombinations.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(
            IGameStateMachine gameStateMachine,
            ServiceContainer.ServiceContainer container,
            ISoundService soundService,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;

            RegisterServices(container, soundService);
        }

        public void Enter() => _gameStateMachine.Enter<LoadProgressState>();

        public void Exit() => SetOrientation();

        private void SetOrientation()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.orientation = ScreenOrientation.AutoRotation;
            //Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
            //Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
        }

        private void RegisterServices(ServiceContainer.ServiceContainer container, ISoundService soundService)
        {
            container.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            container.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            container.RegisterSingle<IEntityContainer>(new EntityContainer());
            container.RegisterSingle<ISaveLoad>(new PrefsSaveLoad());
            container.RegisterSingle<IPersistentProgress>(new PersistentPlayerProgress(container.Single<ISaveLoad>()));
            container.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
            container.RegisterSingle<ISceneLoader>(new SceneLoader());
            RegisterStaticData(container);
            container.RegisterSingle<IDiceCombinationsCalculator>(new DiceCombinationsCalculator(container.Single<IStaticData>().GameConfig));
            container.RegisterSingle<ISoundService>(soundService);
            RegisterUIFactory(container, soundService);
            RegisterGameFactory(container, soundService);
        }

        private void RegisterStaticData(ServiceContainer.ServiceContainer container)
        {
            IStaticData staticData = new StaticData(container.Single<IStaticDataProvider>());
            staticData.LoadStaticData();
            container.RegisterSingle<IStaticData>(staticData);
        }

        private void RegisterUIFactory(ServiceContainer.ServiceContainer container, ISoundService soundService) =>
            container.RegisterSingle<IUIFactory>(new UIFactory(container.Single<IStaticData>(),
                container.Single<IEntityContainer>(), container.Single<IPersistentProgress>(), soundService));

        private void RegisterGameFactory(ServiceContainer.ServiceContainer container, ISoundService soundService) => 
            container.RegisterSingle<IGameFactory>(new GameFactory(container.Single<IStaticData>(), container.Single<IEntityContainer>(), soundService));
    }
}