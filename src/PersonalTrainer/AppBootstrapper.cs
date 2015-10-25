﻿using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Metro;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Voice;
using Figroll.PersonalTrainer.ViewModels;
using ScriptCs.Contracts;
using StructureMap;

namespace Figroll.PersonalTrainer
{
    public class AppBootstrapper : CaliburnMetroCompositionBootstrapper<AppViewModel>
    {
        private IContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new Container(x =>
            {
                x.For<IWindowManager>().Singleton().Use<AppWindowManager>();

                x.For<ITrainingSession>().Singleton().Use<TrainingSession>();
                x.Forward<ITrainingSession, IScriptPackContext>();

                x.For<ITrainerVoice>().Use<TrainerVoice>();

                x.For<IScriptPack>().Singleton().Use<PersonalTrainerScriptPack>();
                x.For<IHostedScriptExecutor>().Use<HostedScriptExecutor>();
            });
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key)
                ? _container.GetInstance(serviceType)
                : _container.GetInstance(serviceType ?? typeof(object), key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}