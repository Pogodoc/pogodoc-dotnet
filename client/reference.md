# Reference
## Templates
<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">InitializeTemplateCreationAsync</a>() -> InitializeTemplateCreationResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Initializes template creation by generating a unique ID and providing a presigned URL for template ZIP upload. Sets unfinished tag for tracking incomplete templates.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.InitializeTemplateCreationAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">SaveCreatedTemplateAsync</a>(templateId, SaveCreatedTemplateRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Finalizes template creation by saving template info to Strapi, copying preview files to permanent storage, and creating template index. Removes unfinished tag upon completion.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.SaveCreatedTemplateAsync(
    "templateId",
    new SaveCreatedTemplateRequest
    {
        TemplateInfo = new SaveCreatedTemplateRequestTemplateInfo
        {
            Title = "title",
            Description = "description",
            Type = SaveCreatedTemplateRequestTemplateInfoType.Docx,
            SampleData = new Dictionary<string, object>() { { "key", "value" } },
            Categories = new List<SaveCreatedTemplateRequestTemplateInfoCategoriesItem>()
            {
                SaveCreatedTemplateRequestTemplateInfoCategoriesItem.Invoice,
            },
        },
        PreviewIds = new SaveCreatedTemplateRequestPreviewIds
        {
            PngJobId = "pngJobId",
            PdfJobId = "pdfJobId",
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `SaveCreatedTemplateRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">UpdateTemplateAsync</a>(templateId, UpdateTemplateRequest { ... }) -> UpdateTemplateResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates template content, handles S3 storage cleanup for old content, updates template metadata in Strapi, and manages preview files. Removes unfinished tags after successful update.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.UpdateTemplateAsync(
    "templateId",
    new UpdateTemplateRequest
    {
        TemplateInfo = new UpdateTemplateRequestTemplateInfo
        {
            Title = "title",
            Description = "description",
            Type = UpdateTemplateRequestTemplateInfoType.Docx,
            SampleData = new Dictionary<string, object>() { { "key", "value" } },
            Categories = new List<UpdateTemplateRequestTemplateInfoCategoriesItem>()
            {
                UpdateTemplateRequestTemplateInfoCategoriesItem.Invoice,
            },
        },
        PreviewIds = new UpdateTemplateRequestPreviewIds
        {
            PngJobId = "pngJobId",
            PdfJobId = "pdfJobId",
        },
        ContentId = "contentId",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateTemplateRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">DeleteTemplateAsync</a>(templateId) -> DeleteTemplateResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a template from Strapi and associated S3 storage. Removes all associated files and metadata.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.DeleteTemplateAsync("templateId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be deleted
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">ExtractTemplateFilesAsync</a>(templateId)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Extracts contents from an uploaded template ZIP file and stores individual files in the appropriate S3 storage structure.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.ExtractTemplateFilesAsync("templateId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be used
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">GenerateTemplatePreviewsAsync</a>(templateId, GenerateTemplatePreviewsRequest { ... }) -> GenerateTemplatePreviewsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates both PNG and PDF preview files for template visualization. Generates previews in parallel and returns URLs for both formats.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.GenerateTemplatePreviewsAsync(
    "templateId",
    new GenerateTemplatePreviewsRequest
    {
        Type = GenerateTemplatePreviewsRequestType.Docx,
        Data = new Dictionary<string, object>() { { "key", "value" } },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be used
    
</dd>
</dl>

<dl>
<dd>

**request:** `GenerateTemplatePreviewsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">GeneratePresignedGetUrlAsync</a>(templateId) -> GeneratePresignedGetUrlResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Generates a presigned URL for template access. Used for downloading template files from S3 storage.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.GeneratePresignedGetUrlAsync("templateId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template that is being downloaded
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">GetTemplateIndexHtmlAsync</a>(templateId) -> GetTemplateIndexHtmlResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieves the template index.html file from S3 storage. Used for rendering the template in the browser.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.GetTemplateIndexHtmlAsync("templateId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be used
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">UploadTemplateIndexHtmlAsync</a>(templateId, UploadTemplateIndexHtmlRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Uploads the template index.html file to S3 storage. Used for rendering the template in the browser.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.UploadTemplateIndexHtmlAsync(
    "templateId",
    new UploadTemplateIndexHtmlRequest { IndexHtml = "indexHtml" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be used
    
</dd>
</dl>

<dl>
<dd>

**request:** `UploadTemplateIndexHtmlRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Templates.<a href="/src/PogodocApi/Templates/TemplatesClient.cs">CloneTemplateAsync</a>(templateId) -> CloneTemplateResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates a new template by duplicating an existing template's content and metadata. Includes copying preview files and template index.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Templates.CloneTemplateAsync("templateId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**templateId:** `string` — ID of the template to be used
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Documents
<details><summary><code>client.Documents.<a href="/src/PogodocApi/Documents/DocumentsClient.cs">InitializeRenderJobAsync</a>(InitializeRenderJobRequest { ... }) -> InitializeRenderJobResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates a new render job with a unique ID, sets up S3 storage for template and data files, and generates presigned upload URLs if needed. Requires subscription check.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Documents.InitializeRenderJobAsync(
    new InitializeRenderJobRequest
    {
        Type = InitializeRenderJobRequestType.Docx,
        Target = InitializeRenderJobRequestTarget.Pdf,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `InitializeRenderJobRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Documents.<a href="/src/PogodocApi/Documents/DocumentsClient.cs">StartRenderJobAsync</a>(jobId, StartRenderJobRequest { ... }) -> StartRenderJobResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Takes a previously initialized job, updates its status to in-progress, and triggers the rendering process using Puppeteer. Can optionally wait for render completion.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Documents.StartRenderJobAsync("jobId", new StartRenderJobRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**jobId:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `StartRenderJobRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Documents.<a href="/src/PogodocApi/Documents/DocumentsClient.cs">StartImmediateRenderAsync</a>(StartImmediateRenderRequest { ... }) -> StartImmediateRenderResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Combines initialization and rendering in one step. Creates a job, uploads template/data directly, starts rendering, and adds the document to Strapi. Requires subscription check.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Documents.StartImmediateRenderAsync(
    new StartImmediateRenderRequest
    {
        StartImmediateRenderRequestData = new Dictionary<string, object>() { { "key", "value" } },
        Type = StartImmediateRenderRequestType.Docx,
        Target = StartImmediateRenderRequestTarget.Pdf,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `StartImmediateRenderRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Documents.<a href="/src/PogodocApi/Documents/DocumentsClient.cs">GetJobStatusAsync</a>(jobId) -> GetJobStatusResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches detailed job information from S3 storage including job status, template ID, target format, and output details if available.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Documents.GetJobStatusAsync("jobId");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**jobId:** `string` — ID of the render job
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>
