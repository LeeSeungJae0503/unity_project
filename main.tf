terraform {
  required_providers {
    google = {
      source  = "hashicorp/google"
      version = "~> 4.0"
    }
  }

  required_version = ">= 1.3.0"
}

provider "google" {
  project = "xenon-aspect-460615-s4"
  region  = "asia-northeast3"
}

resource "google_storage_bucket" "unity_build_bucket" {
  name                        = "gwangju-run-builds-gcp-df3cc8c58d3b6e32"
  location                    = "asia-northeast3"
  force_destroy               = true
  uniform_bucket_level_access = true

  versioning {
    enabled = false
  }

  lifecycle_rule {
    action {
      type = "Delete"
    }
    condition {
      age = 30
    }
  }
}

resource "google_storage_bucket_object" "unity_build" {
  name   = "builds/UnityApp.x86_64"
  bucket = google_storage_bucket.unity_build_bucket.name
  source = "Build/LinuxBuild/UnityApp.x86_64"
}
