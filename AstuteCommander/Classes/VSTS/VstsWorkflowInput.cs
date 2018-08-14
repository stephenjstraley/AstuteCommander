using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsWorkflowInput
    {
        [JsonProperty(PropertyName = "containerregistrytype")]
        public string Containerregistrytype { get; set; }

        [JsonProperty(PropertyName = "dockerRegistryEndpoint")]
        public string DockerRegistryEndpoint { get; set; }

        [JsonProperty(PropertyName = "azureSubscriptionEndpoint")]
        public string AzureSubscriptionEndpoint { get; set; }

        [JsonProperty(PropertyName = "azureContainerRegistry")]
        public string AzureContainerRegistry { get; set; }

        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "dockerFile")]
        public string DockerFile { get; set; }

        [JsonProperty(PropertyName = "buildArguments")]
        public string BuildArguments { get; set; }

        [JsonProperty(PropertyName = "defaultContext")]
        public string DefaultContext { get; set; }

        [JsonProperty(PropertyName = "context")]
        public string Context { get; set; }

        [JsonProperty(PropertyName = "imageName")]
        public object ImageName { get; set; }

        [JsonProperty(PropertyName = "imageNamesPath")]
        public string ImageNamesPath { get; set; }

        [JsonProperty(PropertyName = "qualifyImageName")]
        public string QualifyImageName { get; set; }

        [JsonProperty(PropertyName = "additionalImageTags")]
        public string AdditionalImageTags { get; set; }

        [JsonProperty(PropertyName = "includeSourceTags")]
        public string IncludeSourceTags { get; set; }

        [JsonProperty(PropertyName = "includeLatestTag")]
        public string IncludeLatestTag { get; set; }

        [JsonProperty(PropertyName = "imageDigestFile")]
        public string ImageDigestFile { get; set; }

        [JsonProperty(PropertyName = "containerName")]
        public string ContainerName { get; set; }

        [JsonProperty(PropertyName = "ports")]
        public string Ports { get; set; }

        [JsonProperty(PropertyName = "volumes")]
        public string Volumes { get; set; }

        [JsonProperty(PropertyName = "envVars")]
        public string EnvVars { get; set; }

        [JsonProperty(PropertyName = "workDir")]
        public string WorkDir { get; set; }

        [JsonProperty(PropertyName = "entrypoint")]
        public string Entrypoint { get; set; }

        [JsonProperty(PropertyName = "containerCommand")]
        public string ContainerCommand { get; set; }

        [JsonProperty(PropertyName = "detached")]
        public string Detached { get; set; }

        [JsonProperty(PropertyName = "restartPolicy")]
        public string RestartPolicy { get; set; }

        [JsonProperty(PropertyName = "restartMaxRetries")]
        public string RestartMaxRetries { get; set; }

        [JsonProperty(PropertyName = "customCommand")]
        public string CustomCommand { get; set; }

        [JsonProperty(PropertyName = "dockerHostEndpoint")]
        public string DockerHostEndpoint { get; set; }

        [JsonProperty(PropertyName = "cwd")]
        public string Cwd { get; set; }

        [JsonProperty(PropertyName = "machinesList")]
        public string MachinesList { get; set; }

        [JsonProperty(PropertyName = "applicationDirectory")]
        public string ApplicationDirectory { get; set; }

        [JsonProperty(PropertyName = "appPoolName")]
        public string AppPoolName { get; set; }


    }
}
