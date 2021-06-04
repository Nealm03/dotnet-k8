#!/bin/bash
[ -z "$CHART" ] && echo "Missing CHART var" && exit 1;
[ -z "$RELEASE_NAME" ] && echo "Missing RELEASE_NAME var" && exit 1;
[ -z "$NAMESPACE" ] && echo "Missing NAMESPACE var" && exit 1;

KUBE_CONTEXT=$(kubectl config current-context)

helm upgrade --install \
  $RELEASE_NAME \
  $CHART \
  --kube-context "${KUBE_CONTEXT}" \
  --namespace="$NAMESPACE" \
  $HELM_ARGS


# helm upgrade --install my-test-app-release .
