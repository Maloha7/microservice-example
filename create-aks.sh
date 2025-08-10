#!/usr/bin/env bash

# Set strict fail conditions
set -euo pipefail

# --- Variables ---
RESOURCE_GROUP="MicroservicesInDotnet"
LOCATION="northeurope"
ACR_NAME="malohacr"
AKS_NAME="MicroservicesInDotnetAKSCluster"

# --- 1. Create resource group ---
az group create \
    --name "$RESOURCE_GROUP" \
    --location "$LOCATION"

# --- 2. Create Azure Container Registry ---
az acr create \
    --resource-group "$RESOURCE_GROUP" \
    --name "$ACR_NAME" \
    --sku Basic

# --- 3. Create AKS cluster and attach ACR ---
az aks create \
    --resource-group "$RESOURCE_GROUP" \
    --name "$AKS_NAME" \
    --node-count 1 \
    --node-vm-size Standard_B2s \
    --enable-addons monitoring \
    --generate-ssh-keys \
    --attach-acr "$ACR_NAME"

# --- 4. Get AKS credentials for kubectl ---
az aks get-credentials \
    --resource-group "$RESOURCE_GROUP" \
    --name "$AKS_NAME"

# --- 5. Confirm cluster is running ---
kubectl get nodes
