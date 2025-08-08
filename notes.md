### Deleting cluster and registry

When you are done with the Kubernetes cluster in AKS, you can delete the cluster as well as the container registry with this command:

```bash
az group delete --name MicroservicesInDotnet --yes --no-wait
```

### Switch between Kubernetes on localhost and azure

Find available conexts with:

```bash
kubectl config get-contexts
```

Switch context with:

```bash
kubectl config use-context <name-of-context>
```
